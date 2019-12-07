using System;

namespace Patterns.Contract
{
    public static class DateTimeExtensions
    {
        public static DateTime RoundUp(this DateTime date, TimeSpan span)
        {
            var delta = (span.Ticks - (date.Ticks % span.Ticks)) % span.Ticks;
            return new DateTime(date.Ticks + delta, date.Kind);
        }

        public static DateTime RoundDown(this DateTime date, TimeSpan span)
        {
            var delta = date.Ticks % span.Ticks;
            return new DateTime(date.Ticks - delta, date.Kind);
        }

        public static DateTime RoundToNearest(this DateTime date, TimeSpan span)
        {
            var delta = date.Ticks % span.Ticks;
            bool roundUp = delta > span.Ticks / 2;

            return roundUp ? date.RoundUp(span) : date.RoundDown(span);
        }

        public static DateTime RoundToSecond(this DateTime date)
        {
            return date.RoundUp(TimeSpan.FromSeconds(1));
        }
    }
}