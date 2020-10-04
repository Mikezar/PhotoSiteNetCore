namespace PhotoSite.ApiService.Data.Common
{
    public class IdResult
    {
        public int Id { get; set; }

        public string? ErrorMessage { get; set; }

        public static IdResult GetOk(int id) => new IdResult {Id = id};

        public static IdResult GetError(string errorMessage) => new IdResult { ErrorMessage = errorMessage };
    }
}