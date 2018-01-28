﻿namespace TimelineExample.Services.Caching
{
    /// <summary>
    /// Defines an interface for an object that supports data caching.
    /// </summary>
    public interface IDataCacheInfo
    {
        /// <summary>
        /// Gets or sets the number of days from the current day (UTC) to weed cached data from.
        /// </summary>
        int DaysToWeedCache { get; set; }
    }
}