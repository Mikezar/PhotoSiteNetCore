using Microsoft.Extensions.Logging;

namespace PhotoSite.Domain.Admin
{
    public sealed class TokenManager : ITokenManager
    {
        private readonly ILogger<TokenManager> _logger;

        public TokenManager(ILogger<TokenManager> logger)
        {
            _logger = logger;
        }

        public Token? CurrentToken { get; private set; }

        public Token New()
        {
            CurrentToken = new Token(Guid.NewGuid().ToString("N"));
            return CurrentToken;
        }

        public void Reset(string token)
        {
            Validate(token);
            CurrentToken = null;
        }

        public bool TryValidate(string token)
        {
            try
            {
                Validate(token);
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                return false;
            }
        }

        private void Validate(string token)
        {
            if (CurrentToken is null)
            {
                throw new InvalidOperationException("Current token is empty");
            }

            CurrentToken.Validate(token);
        }
    }
}