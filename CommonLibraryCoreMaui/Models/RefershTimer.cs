using System;
using System.Threading;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CommonLibraryCoreMaui.Models
{
	public class RefershTimer
	{
		private readonly TimeSpan timespan;
		private readonly Action callback;

		private CancellationTokenSource cancellationSource;
		private CancellationToken cancellationToken;

		public RefershTimer(TimeSpan timespan, Action callback)
		{
			this.timespan = timespan;
			this.callback = callback;
		}

		public RefershTimer(TimeSpan timespan, Action callback, CancellationToken superCancellationToken)
		{
			this.timespan = timespan;
			this.callback = callback;
			this.cancellationToken = superCancellationToken;
		}

		public void Start()
		{
			cancellationSource = new CancellationTokenSource();
			// TODO Xamarin.Forms.Device.StartTimer is no longer supported. Use Microsoft.Maui.Dispatching.DispatcherExtensions.StartTimer instead. For more details see https://learn.microsoft.com/en-us/dotnet/maui/migration/forms-projects#device-changes
			Device.StartTimer(this.timespan,
				() => {
					if (cancellationSource.IsCancellationRequested || cancellationToken.IsCancellationRequested)
						return false;
					this.callback.Invoke();
					return true;
				});
		}

		public void Stop()
		{
			cancellationSource?.Cancel();
		}
	}
}
