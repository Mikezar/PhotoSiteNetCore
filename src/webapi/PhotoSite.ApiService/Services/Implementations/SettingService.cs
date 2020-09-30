using System;
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

        public async Task<Settings> GetSettings()
        {
            var context = _factory.GetReadContext();
            var values = await context.SiteSettings.ToArrayAsync();

            var settings = new Settings();
            var fields = typeof(Settings).GetFields();

            foreach (var value in values)
            {
                if (value == null)
                    continue;
                
                var field = fields.FirstOrDefault(t => t.Name == value.Name);
                if (field == null)
                    continue;

                object? val = null;
                if (field.FieldType == typeof(int))
                {
                    if (int.TryParse(value.Value, out var result))
                        val = result;
                    else
                        Logger.Error($"Value '{value.Value}' of '{value.Name}' not parser!");
                }
                else if (field.FieldType == typeof(string))
                {
                    val = value.Value;
                }
                else
                {
                    throw new NotSupportedException($"Type of {field.FieldType} not supported in setting's parser");
                }
                field.SetValue(settings, val);
            }

            return settings;
        }

        public async Task SaveSettings(Settings settings)
        {
            var context = _factory.GetWriteContext();
            var fields = typeof(Settings).GetFields();
            foreach (var field in fields)
            {
                var setting = await context.SiteSettings.FirstOrDefaultAsync(t => t.Name == field.Name);
                string? value = field.GetValue(settings)?.ToString();
                if (setting is null)
                    await context.AddAsync(new SiteSettings() {Name = field.Name, Value = value});
                else
                    setting.Value = value;
            }
            context.SaveChanges();
        }
    }
}