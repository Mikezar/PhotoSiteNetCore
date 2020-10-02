using System;
using Serilog;

namespace PhotoSite.ApiService.Helpers
{
    public static class AdminHelper
    {
        private static readonly ILogger Logger = new LoggerConfiguration().CreateLogger();

        /// <summary>
        /// LifeTime of Token (min)
        /// </summary>
        private const int TokenLifeTime = 20;

        private static string? _currentAdminToken;
        private static DateTimeOffset _timeTokenOut;

        internal static string GetNewAdminToken()
        {
            _currentAdminToken = Guid.NewGuid().ToString("N");
            _timeTokenOut = DateTimeOffset.Now;
            return _currentAdminToken;
        }

        internal static void ResetToken()
        {
            _currentAdminToken = null;
        }

        public static bool CheckToken(string token)
        {
            if (_currentAdminToken is null)
            {
                Logger.Warning("Current local token is empty");
                return false;
            }

            if (DateTimeOffset.Now > _timeTokenOut.AddMinutes(TokenLifeTime))
            {
                Logger.Information("Token is obsolete");
                _currentAdminToken = null;
                return false;
            }

            if (_currentAdminToken != token)
            {
                Logger.Warning("Token is incorrect");
                return false;
            }

            return true;
        }
    }
}