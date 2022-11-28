namespace PhotoSite.Domain.Admin.Exceptions
{
    public class InvalidTokenException : InvalidOperationException
    {
        public InvalidTokenException() : base("Token is incorrect")
        {

        }
    }
}