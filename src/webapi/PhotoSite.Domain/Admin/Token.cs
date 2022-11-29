using PhotoSite.Domain.Admin.Exceptions;

namespace PhotoSite.Domain.Admin;

public class Token
{
    private const int TokenLifeTime = 20;

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

        if (!string.Equals(Value, token, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new InvalidTokenException();
        }
    }
}
