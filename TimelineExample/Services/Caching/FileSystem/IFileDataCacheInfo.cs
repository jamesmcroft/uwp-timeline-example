namespace TimelineExample.Services.Caching.FileSystem
{
    /// <summary>
    /// Defines an interface for an object that supports file data caching.
    /// </summary>
    public interface IFileDataCacheInfo
    {
        /// <summary>
        /// Gets the name of the folder storing application data.
        /// </summary>
        string ApplicationFolderName { get; }

        /// <summary>
        /// Gets the name of the folder storing the cached data.
        /// </summary>
        string CacheFolderName { get; }
    }
}