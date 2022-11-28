namespace PhotoSite.Domain.Admin
{
    public interface ITokenManager
    {
        Token CurrentToken { get; }
        Token New();
        void Reset(string token);
        bool TryValidate(string token);
    }
}