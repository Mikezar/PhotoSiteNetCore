using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using PhotoSite.ApiService.Data.Admin;
using PhotoSite.ApiService.Helpers;
using PhotoSite.ApiService.Services.Interfaces;

namespace PhotoSite.ApiService.Services.Implementations
{
    public class AdminService : IAdminService
    {

        private const string Salt = "04287032E7E04596BE279F083D42FDA5";

        private readonly string _login;
        private readonly string _passwordHash;
        
        public AdminService(IOptionsSnapshot<LoginOptions> config)
        {
            _login = config.Value.Login;
            _passwordHash = config.Value.Password;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        /// <returns>Login state</returns>
        public LoginState Login(string login, string password)
        {
            if (login != _login)
                return LoginState.GetErrorState(LoginStatus.InvalidPasswordOrLogin);

            var hashValue = GetPasswordHash(login, password);

            if (_passwordHash != hashValue)
                return LoginState.GetErrorState(LoginStatus.InvalidPasswordOrLogin);

            // TODO: Другие проверки (кол-во попыток входа, с какого ip и пр.)

            var token = AdminHelper.GetNewAdminToken();

            return new LoginState(LoginStatus.Success, token);
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="token">Token</param>
        public void Logout(string token)
        {
            if (AdminHelper.ValidateToken(token))
                AdminHelper.ResetToken();
        }

        /// <summary>
        /// Create password's hash
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>password's hash</returns>
        /// <remarks>Not for api!</remarks>
        public string GetPasswordHash(string login, string password)
        {
            var bts = Encoding.ASCII.GetBytes(String.Concat(login, Salt, password));
            var tmpHash = new SHA512Managed().ComputeHash(bts);
            return Convert.ToBase64String(tmpHash);
        }



        //private static string ByteArrayToString(byte[] arrInput)
        //{
        //    int i;
        //    StringBuilder sOutput = new StringBuilder(arrInput.Length);
        //    for (i = 0; i < arrInput.Length - 1; i++)
        //        sOutput.Append(arrInput[i].ToString("X2"));
        //    return sOutput.ToString();
        //}
    }
}