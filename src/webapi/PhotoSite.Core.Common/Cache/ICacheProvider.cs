using System.Threading;
using System.Threading.Tasks;

namespace PhotoSite.Core.Cache
{
    public interface ICacheProvider
    {
        Task Set<TKey, TValue>(TKey key, TValue value, CacheExpiration expiration,
            CancellationToken? cancellationToken = null);

        Task Remove<TKey>(TKey key) where TKey : notnull;

        Task<TValue> Get<TKey, TValue>(TKey key) where TKey : notnull;
    }
}
