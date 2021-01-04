using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace PhotoSite.Core.Cache
{
    public abstract class SimpleCache<TValue> : ICache, IDisposable
    {
        private readonly IMemoryCache _cache;
        private readonly CacheExpiration _expiration;

        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1,1); // Skip race
        private readonly TimeSpan _lockTimeout = TimeSpan.FromMinutes(1); // just in case

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        protected SimpleCache(IMemoryCache cache, CacheExpiration? expiration = null)
        {
            _expiration = expiration ?? CacheExpiration.Default;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public virtual string Key => GetType().FullName!;

        protected void Put(TValue item)
        {
            Set(item);
        }

        protected TValue Get()
        {
            return _cache.Get<TValue>(Key);
        }

        protected async Task<TValue> GetOrAdd(Func<Task<TValue>> addItem)
        {
            return await GetOrAddInternal(addItem);
        }

        private async Task<TValue> GetOrAddInternal(Func<Task<TValue>> addItem)
        {
            var comparer = EqualityComparer<TValue>.Default;
            var value = _cache.Get<TValue>(Key);
            if (comparer.Equals(value, default))
            {
                if (!await _lock.WaitAsync(_lockTimeout))
                    throw new Exception($"Error get lock of refresh cache {Key}");
                try
                {
                    value = _cache.Get<TValue>(Key);
                    if (comparer.Equals(value, default))
                    {
                        value = await addItem();
                        Set(value);
                    }
                }
                finally
                {
                    _lock.Release();
                }
            }

            return value;
        }

        public void Remove()
        {
            _cache.Remove(Key);
        }

        public virtual void TryFlush()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        private void Set(TValue value)
        {
            var options = GetOptions(_expiration);
            options = options.AddExpirationToken(new CancellationChangeToken(_cancellationTokenSource.Token));
            _cache.Set(Key, value, options);
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