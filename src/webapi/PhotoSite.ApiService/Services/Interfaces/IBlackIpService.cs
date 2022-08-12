using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSite.ApiService.Base;
using PhotoSite.ApiService.Data;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Services.Interfaces
{
    public interface IBlackIpService : IService
    {
        /// <summary>
        /// Get all ip v4
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BlackInterNetworkV4>> GetV4();

        /// <summary>
        /// Get all ip v6
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BlackInterNetworkV6>> GetV6();

        /// <summary>
        /// Get all IP
        /// </summary>
        /// <returns>All IP</returns>
        Task<IEnumerable<BlackIp>> GetAll();

        /// <summary>
        /// Add IP
        /// </summary>
        /// <param name="blackIp"></param>
        /// <returns>Key</returns>
        Task<IIdResult> Create(BlackIp blackIp);

        /// <summary>
        /// Delete IP
        /// </summary>
        /// <param name="id">Key</param>
        Task Delete(int id);

        /// <summary>
        /// Update IP
        /// </summary>
        /// <param name="blackIp"></param>
        /// <returns></returns>
        Task Update(BlackIp blackIp);
    }
}