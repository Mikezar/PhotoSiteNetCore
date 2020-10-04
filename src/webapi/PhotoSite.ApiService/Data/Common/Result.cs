namespace PhotoSite.ApiService.Data.Common
{
    public class Result
    {
        public string? ErrorMessage { get; set; }

        public static Result GetOk() => new Result();

        public static Result GetError(string errorMessage) => new Result { ErrorMessage = errorMessage };
    }
}