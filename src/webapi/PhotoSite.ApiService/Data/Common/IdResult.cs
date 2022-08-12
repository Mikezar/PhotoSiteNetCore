namespace PhotoSite.ApiService.Data.Common
{
    public class IdResult : IIdResult
    {
        public int Id { get; set; }

        public IdResult(int id)
        {
            Id = id;  
        }
    }
}