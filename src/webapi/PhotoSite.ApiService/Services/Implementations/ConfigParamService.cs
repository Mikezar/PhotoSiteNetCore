using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using PhotoSite.ApiService.Data;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;
using Serilog;

namespace PhotoSite.ApiService.Services.Implementations
{
    public class ConfigParamService : IConfigParamService
    {
        private static readonly ILogger Logger = new LoggerConfiguration().CreateLogger();

        private Settings? _settings;

        private static readonly object Locker = new object();

        private readonly IConfigParamRepository _configParamRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public ConfigParamService(IConfigParamRepository configParamRepository)
        {
            _configParamRepository = configParamRepository;
        }

        /// <summary>
        /// Get default settings
        /// </summary>
        /// <returns>Default settings</returns>
        public Settings GetDefaultSettings()
        {
            var settings = new Settings();
            var properties = typeof(Settings).GetProperties();
            foreach (var property in properties)
            {
                var pdc = TypeDescriptor.GetProperties(settings)[property.Name];
                if (pdc is null)
                    continue;
                AttributeCollection attributes = pdc.Attributes;
                var attr = attributes[typeof(DefaultValueAttribute)];
                if (attr is null)
                    continue;
                DefaultValueAttribute defAttribute = (DefaultValueAttribute)attr;
                property.SetValue(settings, defAttribute.Value);
            }
            return settings;
        }

        public async Task<Settings> GetSettings()
        {
            if (_settings is null)
            {
                var settings = await LoadSettings();
                lock (Locker)
                    _settings = settings;
            }
            return _settings;
        }


        private async Task<Settings> LoadSettings()
        {
            var values = await _configParamRepository.GetAsNoTrackingAll();

            var settings = new Settings();
            var properties = typeof(Settings).GetProperties();

            foreach (var value in values)
            {
                if (value is null)
                    continue;
                
                var property = properties.FirstOrDefault(t => t.Name == value.Name);
                if (property is null)
                    continue;
                
                object? val = null;
                if (property.PropertyType == typeof(int))
                {
                    if (int.TryParse(value.Value, out var result))
                        val = result;
                    else
                        Logger.Error($"Value '{value.Value}' of '{value.Name}' not parser!");
                }
                else if (property.PropertyType == typeof(string))
                {
                    val = value.Value;
                }
                else
                {
                    throw new NotSupportedException($"Type of {property.PropertyType} not supported in setting's parser");
                }

                property.SetValue(settings, val);
            }

            foreach (var property in properties)
            {
                var value = property.GetValue(settings);
                if (property.PropertyType == typeof(string) && value is not null
                    ||
                    property.PropertyType == typeof(int) && (int?)value != 0)
                    continue;
                var pdc = TypeDescriptor.GetProperties(settings)[property.Name];
                if (pdc is null)
                    continue;
                AttributeCollection attributes = pdc.Attributes;
                var aatr = attributes[typeof(DefaultValueAttribute)];
                if (aatr is null)
                    continue;
                DefaultValueAttribute defAttribute = (DefaultValueAttribute)aatr;
                property.SetValue(settings, defAttribute.Value);
            }

            return settings;
        }

        public async Task SaveSettings(Settings settings)
        {
            var properties = typeof(Settings).GetProperties();
            foreach (var property in properties)
            {
                var setting = await _configParamRepository.Get(property.Name);
                string? value = property.GetValue(settings)?.ToString();
                if (setting is null)
                    await _configParamRepository
                        .Create(new ConfigParam() { Name = property.Name, Value = value });
                else
                    setting.Value = value;
            }

            await ((IDbContext) _configParamRepository).SaveChanges();

            lock (Locker)
                _settings = settings;
        }
    }
}