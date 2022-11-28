namespace PhotoSite.Domain.Admin
{
    public static class TokenManager
    {
        private const int TokenLifeTime = 20;

        public static Token New()
        {
            CurrentToken = new Token(Guid.NewGuid().ToString("N"));
            return CurrentToken;
        }
        public static void Reset()
        {
            CurrentToken = null;
        }

        public static Token? CurrentToken { get; private set; }

        public static void Check(string token)
        {
            if (CurrentToken is null)
            {
                throw new InvalidOperationException("Current token is empty");
            }

            CurrentToken.Validate(token);
        }

        public class Token
        {
            public string Value { get; }
            public DateTimeOffset ExpiresAt { get; }

            public Token(string value)
            {
                Value = value;
                ExpiresAt = DateTimeOffset.Now;
            }

            internal void Validate(string token)
            {
                if (DateTimeOffset.Now > ExpiresAt.AddMinutes(TokenLifeTime))
                {
                    throw new ObsoleteTokenException();
                }

                if (string.Equals(Value, token, StringComparison.CurrentCultureIgnoreCase))
                {
                    throw new InvalidTokenException();
                }
            }
        }

        public class ObsoleteTokenException : InvalidOperationException
        {
            public ObsoleteTokenException() : base("Token is obsolete")
            {

            }
        }

        public class InvalidTokenException : InvalidOperationException
        {
            public InvalidTokenException() : base("Token is incorrect")
            {

            }
        }
    }
}