using System;
namespace CommonLibraryCoreMaui
{
    public static class WaitTimeUtility
    {
        public static string DisplayWaitTime(TimeSpan ts)
        {
            int minutes = ((int)ts.TotalMinutes) + (ts.Seconds > 0 ? 1 : 0);
            if (minutes == 0) minutes = 1;
            return $"or try again in {minutes} {(minutes > 1 ? "minutes" : "minute")}.";
        }
    }
}
