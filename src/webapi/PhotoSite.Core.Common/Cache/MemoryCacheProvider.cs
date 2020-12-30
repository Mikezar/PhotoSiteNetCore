using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace PhotoSite.Core.Cache
{
    public sealed class MemoryCacheProvider : ICacheProvider
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheProvider(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public Task Set<TKey, TValue>(TKey key, TValue value, CacheExpiration expiration, CancellationToken? cancellationToken = null)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var options = GetOptions(expiration);
            if (cancellationToken is not null)
                options = options.AddExpirationToken(new CancellationChangeToken(cancellationToken.Value));
            _cache.Set(key, value, options);
            return Task.CompletedTask;
        }

        public Task Remove<TKey>(TKey key) where TKey : notnull
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));

            _cache.Remove(key);
            return Task.CompletedTask;
        }

        public Task<TValue> Get<TKey, TValue>(TKey key) where TKey : notnull
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));

            return Task.FromResult(_cache.Get<TValue>(key));
        }

        private static MemoryCacheEntryOptions GetOptions(CacheExpiration expiration)
        {
            if (expiration is null)
                throw new ArgumentNullException(nameof(expiration));
            var options = new MemoryCacheEntryOptions();
            if (expiration.AbsoluteExpiration.HasValue)
                options.SetAbsoluteExpiration(expiration.AbsoluteExpiration.Value);
            if (expiration.RelativeExpiration.HasValue)
                options.SetAbsoluteExpiration(expiration.RelativeExpiration.Value);
            if (expiration.SlidingExpiration.HasValue)
                options.SetSlidingExpiration(expiration.SlidingExpiration.Value);
            return options;
        }
    }
}