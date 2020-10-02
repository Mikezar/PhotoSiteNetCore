using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSite.ApiService.Data;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;
using Serilog;

namespace PhotoSite.ApiService.Services.Implementations
{
    public class SettingService : ISettingService
    {
        private static readonly ILogger Logger = new LoggerConfiguration().CreateLogger();

        private readonly DbFactory _factory;

        public SettingService(DbFactory factory)
        {
            _factory = factory;
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
                AttributeCollection attributes = TypeDescriptor.GetProperties(settings)[property.Name].Attributes;
                DefaultValueAttribute defAttribute = (DefaultValueAttribute)attributes[typeof(DefaultValueAttribute)];
                property.SetValue(settings, defAttribute.Value);
            }
            return settings;
        }

        public async Task<Settings> GetSettings()
        {
            var context = _factory.GetReadContext();
            var values = await context.SiteSettings.ToArrayAsync();

            var settings = new Settings();
            var properties = typeof(Settings).GetProperties();

            foreach (var value in values)
            {
                if (value == null)
                    continue;
                
                var property = properties.FirstOrDefault(t => t.Name == value.Name);
                if (property == null)
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
                if (property.PropertyType == typeof(string) && value != null
                    ||
                    property.PropertyType == typeof(int) && (int?)value != 0)
                    continue;
                AttributeCollection attributes = TypeDescriptor.GetProperties(settings)[property.Name].Attributes;
                DefaultValueAttribute defAttribute = (DefaultValueAttribute)attributes[typeof(DefaultValueAttribute)];
                property.SetValue(settings, defAttribute.Value);
            }

            return settings;
        }

        public async Task SaveSettings(Settings settings)
        {
            await using var context = _factory.GetWriteContext();
            var properties = typeof(Settings).GetProperties();
            foreach (var property in properties)
            {
                var setting = await context.SiteSettings.FirstOrDefaultAsync(t => t.Name == property.Name);
                string? value = property.GetValue(settings)?.ToString();
                if (setting is null)
                    await context.AddAsync(new SiteSettings() {Name = property.Name, Value = value});
                else
                    setting.Value = value;
            }

            await context.SaveChangesAsync();
        }
    }
}