using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoSite.Data.Base
{
    public interface ICrudRepository<TEntity, TKey> : IRepository
    where TEntity : class
    {
        Task<TKey> Create(TEntity e, bool save = true);

        Task Delete(TKey id, bool save = true);

        Task Delete(TEntity e, bool save = true);

        Task Update(TEntity e, bool save = true);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity?> Get(TKey id);

        Task<TEntity?> GetAsNoTracking(TKey id);
    }
}