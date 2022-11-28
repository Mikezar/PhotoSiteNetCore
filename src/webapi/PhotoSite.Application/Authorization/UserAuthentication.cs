using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace PhotoSite.Domain.Admin;

internal sealed class UserAuthentication : IUserAuthentication
{
    private const string Salt = "04287032E7E04596BE279F083D42FDA5";

    private readonly ITokenManager _tokenManager;
    private readonly string _login;
    private readonly string _password;

    public UserAuthentication(
        IOptions<LoginOptions> loginOptions,
        ITokenManager tokenManager)
	{
        _login = loginOptions.Value.Login;
        _password = loginOptions.Value.Password;
        _tokenManager = tokenManager;
    }

    public LoginState Login(string login, string password)
    {
        if (!string.Equals(_login, login, StringComparison.CurrentCultureIgnoreCase))
        {
            return LoginState.ErrorState;
        }

        var hashValue = GetPasswordHash(login, password);

        if (!string.Equals(_password, hashValue, StringComparison.CurrentCultureIgnoreCase))
        {
            return LoginState.ErrorState;
        }

        var token = _tokenManager.New();
        return LoginState.SuccessState(token);
    }

    public void Logout(string token)
    {
        _tokenManager.Reset(token);
    }

    private static string GetPasswordHash(string login, string password)
    {
        var bts = Encoding.UTF8.GetBytes(string.Concat(login, Salt, password));
        using var sha512 = SHA512.Create();
        var hash = sha512.ComputeHash(bts);
        return Convert.ToBase64String(hash);
    }
}
