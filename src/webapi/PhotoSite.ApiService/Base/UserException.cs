using System;

namespace PhotoSite.ApiService.Base
{
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {

        }
    }
}