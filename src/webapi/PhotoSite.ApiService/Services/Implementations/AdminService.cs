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
        private const string LoginDefault = "Default";

        private const string Salt = "04287032E7E04596BE279F083D42FDA5";

        private readonly string _login;
        private readonly string _passwordHash;
        
        public AdminService(IOptionsSnapshot<LoginOptions> config)
        {
            _login = config.Value.Login ?? LoginDefault;
            _passwordHash = config.Value.Password ?? String.Empty;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        /// <returns>Login state</returns>
        public LoginState Login(string? login, string? password)
        {
            if (login is null || password is null || login != _login)
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
        public void Logout(string? token)
        {
            if (token != null && AdminHelper.CheckToken(token))
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
            var bts = Encoding.UTF8.GetBytes(String.Concat(login, Salt, password));
            using var alg = SHA512.Create();
            var tmpHash = alg.ComputeHash(bts);
            return Convert.ToBase64String(tmpHash);

            //string hex = "";
            //var hashValue = alg.ComputeHash(bts);
            //foreach (byte x in hashValue)
            //    hex += String.Format("{0:x2}", x);
            //return hex;
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