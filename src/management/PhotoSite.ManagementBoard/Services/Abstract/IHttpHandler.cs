using PhotoSite.ManagementBoard.Models;
using System.Threading.Tasks;

namespace PhotoSite.ManagementBoard.Services.Abstract
{
    internal interface IHttpHandler
    {
        Task<ResultWrapper<TResult>> GetAsync<TResult>(string method);
        Task<NoResultWrapper> PostAsync(string method);
        Task<NoResultWrapper> PostAsync(string method, object model);
        Task<ResultWrapper<TResult>> PostAsync<TResult>(string method, object model);
        Task<NoResultWrapper> PutAsync(string method, object model);
    }
}
