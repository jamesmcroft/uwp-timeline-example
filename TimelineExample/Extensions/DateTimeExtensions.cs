namespace TimelineExample.Extensions
{
    using System;

    /// <summary>
    /// Defines a collection of common extensions for <see cref="DateTime"/> objects.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Compares a <see cref="DateTime"/> value to see if it is less than the given date.
        /// </summary>
        /// <param name="dateTime">
        /// The <see cref="DateTime"/> to check is less than.
        /// </param>
        /// <param name="minDate">
        /// The <see cref="DateTime"/> to compare with.
        /// </param>
        /// <param name="includeTime">
        /// A value indicating whether to include the time component.
        /// </param>
        /// <returns>
        /// Returns true if <paramref name="dateTime"/> is less than <paramref name="minDate"/>.
        /// </returns>
        public static bool IsLessThan(this DateTime dateTime, DateTime minDate, bool includeTime)
        {
            DateTime dateTime1 = dateTime.AddSeconds(-1 * dateTime.Second);
            DateTime dateTime2 = minDate.AddSeconds(-1 * minDate.Second);

            if (dateTime1.Date < dateTime2.Date)
            {
                return true;
            }

            if (!includeTime)
            {
                return false;
            }

            if (dateTime1.Date != dateTime2.Date)
            {
                return false;
            }

            TimeSpan timeSpan1 = new TimeSpan(dateTime1.Hour, dateTime1.Minute, 0);
            TimeSpan timeSpan2 = new TimeSpan(dateTime2.Hour, dateTime2.Minute, 0);

            return timeSpan1 < timeSpan2;
        }
    }
}