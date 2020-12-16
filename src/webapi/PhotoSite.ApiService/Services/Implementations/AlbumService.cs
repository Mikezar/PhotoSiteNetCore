using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoSite.ApiService.Base;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Base;
using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Services.Implementations
{
    public class AlbumService : DbServiceBase, IAlbumService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AlbumService(MainDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Get child albums
        /// </summary>
        /// <param name="parentId">Parent album's identification</param>
        /// <returns>Albums</returns>
        public async Task<Album[]> GetChild(int? parentId)
        {
            return await DbContext.Albums!.Where(t => t.ParentId == parentId).ToArrayAsync();
        }

    }
}