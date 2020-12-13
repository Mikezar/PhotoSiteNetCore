namespace PhotoSite.ApiService.Data.Common
{
    public class Result : IResult
    {
        public string? ErrorMessage { get; set; }

        public static IResult GetOk() => new Result();

        public static IResult GetError(string errorMessage) => new Result { ErrorMessage = errorMessage };
    }
}