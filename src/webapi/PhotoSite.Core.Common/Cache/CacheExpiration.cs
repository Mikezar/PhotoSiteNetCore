using System;

namespace PhotoSite.Core.Cache
{
    /// <summary>
    /// Timeouts for cache
    /// </summary>
    public class CacheExpiration
    {
        private const int DefaultExpirationInMinutes = 30;

        private CacheExpiration()
        {
        }

        /// <summary>
        /// Expiration at time
        /// </summary>
        public DateTimeOffset? AbsoluteExpiration { private set; get; }

        /// <summary>
        /// Expiration after add to cache
        /// </summary>
        public TimeSpan? RelativeExpiration { private set; get; }

        /// <summary>
        /// Expiration after last access
        /// </summary>
        public TimeSpan? SlidingExpiration { private set; get; }

        /// <summary>
        /// Expiration at time
        /// </summary>
        public static CacheExpiration Absolute(DateTimeOffset absoluteExpiration, TimeSpan? slidingExpiration = null)
        {
            return new CacheExpiration { AbsoluteExpiration = absoluteExpiration, SlidingExpiration = slidingExpiration };
        }

        /// <summary>
        /// Expiration after add to cache
        /// </summary>
        public static CacheExpiration Relative(TimeSpan relativeExpiration, TimeSpan? slidingExpiration = null)
        {
            return new CacheExpiration { RelativeExpiration = relativeExpiration, SlidingExpiration = slidingExpiration };
        }

        /// <summary>
        /// Expiration after last access
        /// </summary>
        public static CacheExpiration Sliding(TimeSpan slidingExpiration)
        {
            return new CacheExpiration { SlidingExpiration = slidingExpiration };
        }

        public static readonly CacheExpiration Default = Relative(TimeSpan.FromMinutes(DefaultExpirationInMinutes));
    }
}