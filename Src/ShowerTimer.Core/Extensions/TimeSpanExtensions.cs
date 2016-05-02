namespace ShowerTimer.Core.Extensions
{
    using System;

    public static class TimeSpanExtensions
    {
        public static bool IsNegative(this TimeSpan time)
        {
            return time.CompareTo(TimeSpan.Zero) < 0;
        }
    }
}
