namespace PhotoSite.Domain.Admin.Exceptions
{
    public class ObsoleteTokenException : InvalidOperationException
    {
        public ObsoleteTokenException() : base("Token is obsolete")
        {

        }
    }
}