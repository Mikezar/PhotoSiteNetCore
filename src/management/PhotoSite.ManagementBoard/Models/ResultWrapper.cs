namespace PhotoSite.ManagementBoard.Models
{
    public class ResultWrapper<TResult>
    {
        public bool IsSuccess { get; set; }
        public TResult Result { get; set; }

        public static ResultWrapper<TResult> CreateFailed()
            => new ResultWrapper<TResult> { IsSuccess = false };
    }

    public class NoResultWrapper
    {
        public bool IsSuccess { get; set; }
    }
}
