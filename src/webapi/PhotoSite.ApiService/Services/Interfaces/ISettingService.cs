﻿using System.Threading.Tasks;
using PhotoSite.ApiService.Base;
using PhotoSite.ApiService.Data;

namespace PhotoSite.ApiService.Services.Interfaces
{
    public interface ISettingService : IService
    {
        /// <summary>
        /// Get site's settings
        /// </summary>
        /// <returns>Settings</returns>
        Task<Settings> GetSettings();

        /// <summary>
        /// Save site's settings
        /// </summary>
        /// <param name="settings">Sites's settings</param>
        Task SaveSettings(Settings settings);
    }
}