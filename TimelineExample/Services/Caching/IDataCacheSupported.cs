namespace TimelineExample.Services.Caching
{
    /// <summary>
    /// Defines an interface for an object that supports data caching.
    /// </summary>
    public interface IDataCacheSupported
    {
        /// <summary>
        /// Gets the provider for the data caching.
        /// </summary>
        IDataCacheProvider CacheProvider { get; }

        /// <summary>
        /// Gets or sets a value indicating whether caching is currently enabled.
        /// </summary>
        bool IsCachingEnabled { get; set; }
    }
}