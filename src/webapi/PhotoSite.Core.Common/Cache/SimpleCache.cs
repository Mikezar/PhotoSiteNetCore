using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoSite.Core.Cache
{
    public abstract class SimpleCache<TKey, TValue> : ICache, IDisposable
    where TKey : notnull
    {
        //private const string DefaultKey = "__def__";

        private readonly ICacheProvider _cacheProvider;
        private readonly CacheExpiration _expiration;
        private readonly TKey _key;

        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1,1); // Skip race
        private readonly TimeSpan _lockTimeout = TimeSpan.FromMinutes(1); // just in case

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        protected SimpleCache(ICacheProvider cacheProvider, TKey key, CacheExpiration? expiration = null)
        {
            _key = key ?? throw new ArgumentNullException(nameof(key));
            _expiration = expiration ?? CacheExpiration.Default;
            _cacheProvider = cacheProvider ?? throw new ArgumentNullException(nameof(cacheProvider));
        }

        public virtual string CacheName => GetType().FullName!;

        private CompositeKey GetKey(TKey key) => new CompositeKey(CacheName, key);

        protected async Task Put(TValue item)
        {
            await _cacheProvider.Set(GetKey(_key), item, _expiration, _cancellationTokenSource.Token);
        }

        protected async Task<TValue> Get()
        {
            return await _cacheProvider.Get<CompositeKey, TValue>(GetKey(_key));
        }

        protected async Task<TValue> GetOrAdd(Func<Task<TValue>> addItem)
        {
            return await GetOrAddInternal(_key, addItem);
        }

        private async Task<TItem> GetOrAddInternal<TItem>(TKey key, Func<Task<TItem>> addItem)
        {
            var comparer = EqualityComparer<TItem>.Default;
            var compositeKey = GetKey(key);
            var value = await _cacheProvider.Get<CompositeKey, TItem>(compositeKey);
            if (comparer.Equals(value, default))
            {
                if (!await _lock.WaitAsync(_lockTimeout))
                    throw new Exception($"Error get lock of refresh cache {CacheName}");
                try
                {
                    value = await _cacheProvider.Get<CompositeKey, TItem>(compositeKey);
                    if (comparer.Equals(value, default))
                    {
                        value = await addItem();
                        await _cacheProvider.Set(compositeKey, value, _expiration, _cancellationTokenSource.Token);
                    }
                }
                finally
                {
                    _lock.Release();
                }
            }

            return value;
        }

        public async Task Remove()
        {
            await _cacheProvider.Remove(GetKey(_key));
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

        private readonly struct CompositeKey
        {
            private static readonly Lazy<TypeConverter> Converter = new Lazy<TypeConverter>(() => TypeDescriptor.GetConverter(typeof(TKey)));

            public CompositeKey(string cacheName, TKey key)
            {
                CacheName = cacheName;
                Key = key;
            }

            private string CacheName { get; }

            private TKey Key { get; }

            public override string ToString() => $"{CacheName}.{Converter.Value.ConvertToInvariantString(Key)}";

            public override bool Equals(object? value)
            {
                return value is CompositeKey key && this == key;
            }

            public static bool operator ==(CompositeKey x, CompositeKey y)
            {
                return x.CacheName == y.CacheName && x.Key.Equals(y.Key);
            }

            public static bool operator !=(CompositeKey x, CompositeKey y)
            {
                return !(x.CacheName == y.CacheName && x.Key.Equals(y.Key));
            }

            public override int GetHashCode()
            {
                return CacheName.GetHashCode() ^ (397 * Key.GetHashCode());
            }
        }
    }
}