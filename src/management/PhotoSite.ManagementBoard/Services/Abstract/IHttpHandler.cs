using PhotoSite.ManagementBoard.Models;
using System.Threading.Tasks;

namespace PhotoSite.ManagementBoard.Services.Abstract
{
    internal interface IHttpHandler
    {
        string Token { get; set; }

        Task<NoResultWrapper> PostAsync(string method, object model);
        Task<ResultWrapper<TResult>> PostAsync<TResult>(string method, object model);
    }
}
