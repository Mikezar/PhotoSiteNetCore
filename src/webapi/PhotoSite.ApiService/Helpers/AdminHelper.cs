using System;

namespace PhotoSite.ApiService.Helpers
{
    public static class AdminHelper
    {
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
            // TODO: Логирование!
            if (_currentAdminToken is null)
                return false;

            if (DateTimeOffset.Now > _timeTokenOut.AddMinutes(TokenLifeTime))
            {
                _currentAdminToken = null;
                return false;
            }

            if (_currentAdminToken != token)
                return false;

            return true;
        }
    }
}