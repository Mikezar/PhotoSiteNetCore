using System;

namespace PhotoSite.Core.ExtException
{
    /// <summary>
    /// Message for user
    /// </summary>
    public class UserException : Exception
    {
        public string UserMessage { get; init; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="userMessage"></param>
        public UserException(string userMessage)
        {
            UserMessage = userMessage;
        }
    }
}
