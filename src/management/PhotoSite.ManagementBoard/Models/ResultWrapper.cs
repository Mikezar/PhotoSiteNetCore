using System.Net;

namespace PhotoSite.ManagementBoard.Models
{
    public class ResultWrapper<TResult> : NoResultWrapper
    {
        public TResult Result { get; set; }

        public static ResultWrapper<TResult> CreateFailed(HttpStatusCode code)
            => new ResultWrapper<TResult> { IsSuccess = false, Code = code };

        public static ResultWrapper<TResult> CreateSuccess(TResult result)
            => new ResultWrapper<TResult> { IsSuccess = true, Code = HttpStatusCode.OK, Result = result };
    }

    public class NoResultWrapper
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode Code { get; set; }
    }
}
