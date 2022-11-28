using System.Security.Cryptography;
using System.Text;

namespace PhotoSite.Domain.Admin;

public sealed class LoginAttempt
{
    private const string Salt = "04287032E7E04596BE279F083D42FDA5";

    private readonly string _login;
    private readonly string _password;

    public LoginAttempt(string login, string password)
	{
        _login = login;
        _password = password;
    }

    public LoginState Login(LoginOptions loginOptions)
    {
        if (!string.Equals(_login, loginOptions.Login, StringComparison.CurrentCultureIgnoreCase))
        {
            return LoginState.ErrorState;
        }

        var hashValue = GetPasswordHash(_login, _password);

        if (!string.Equals(hashValue, loginOptions.Password, StringComparison.CurrentCultureIgnoreCase))
        {
            return LoginState.ErrorState;
        }

        var token = TokenManager.New();
        return LoginState.SuccessState(token);
    }

    private static string GetPasswordHash(string login, string password)
    {
        var bts = Encoding.UTF8.GetBytes(string.Concat(login, Salt, password));
        using var sha512 = SHA512.Create();
        var hash = sha512.ComputeHash(bts);
        return Convert.ToBase64String(hash);
    }
}
