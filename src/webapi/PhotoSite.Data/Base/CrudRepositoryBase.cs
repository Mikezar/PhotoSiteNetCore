using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PhotoSite.Data.Base
{
    public abstract class CrudRepositoryBase<TObject, TKey> : RepositoryBase, ICrudRepository<TObject, TKey>
        where TObject : class, IEntityBase<TKey>, new()
    {
        private readonly DbSet<TObject> _dbSet;

        protected CrudRepositoryBase(MainDbContext dbContext) : base(dbContext)
        {
            _dbSet = DbContext.Set<TObject>();
        }

        public async Task<TKey> Create(TObject e, bool save = true)
        {
            await DbContext.AddAsync(e);
            if (save)
                await DbContext.SaveChangesAsync();
            return e.Id;
        }

        public async Task Update(TObject e, bool save = true)
        {
            DbContext.Attach(e);
            DbContext.Update(e);
            if (save)
                await DbContext.SaveChangesAsync();
        }
        
        public async Task Delete(TKey id, bool save = true)
        {
            await Delete(new TObject {Id = id}, save);
        }

        public async Task Delete(TObject e, bool save = true)
        {
            DbContext.Remove(e);
            if (save)
                await DbContext.SaveChangesAsync();
        }

        public async Task<TObject?> Get(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TObject?> GetAsNoTracking(TKey id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(t => Equals(t.Id, id));
        }

        public async Task<ICollection<TObject>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToArrayAsync();
        }

    }
}