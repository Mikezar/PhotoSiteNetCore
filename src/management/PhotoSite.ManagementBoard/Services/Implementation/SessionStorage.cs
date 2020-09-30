using System;

namespace PhotoSite.ManagementBoard.Services.Implementation
{
    internal class SessionStorage
    {
        public bool IsAuth { get; private set; }
        public string Token { get; private set; }

        public SessionStorage Auth(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));

            IsAuth = true;
            Token = token;

            return this;
        }

        public SessionStorage Clean()
        {
            IsAuth = false;
            Token = null;

            return this;
        }
    }
}
