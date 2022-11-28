namespace PhotoSite.Domain.Admin;

public interface IUserAuthentication
{
    LoginState Login(string login, string password);
    void Logout(string token);
}
