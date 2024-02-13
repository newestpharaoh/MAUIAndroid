using System;

namespace CommonLibraryCoreMaui
{
    public class LockoutTimeEventArgs : EventArgs
    {
        public double Minutes { get; set; }

        public LockoutTimeEventArgs(double minutes)
        {
            this.Minutes = minutes;
        }
    }
}
