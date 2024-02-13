using System;
namespace CommonLibraryCoreMaui.DependencyServices
{
	public interface IPlatformFeatures
	{
		void GoToRootView(Action completionAction);
	}

	public interface IAudioService
	{
		void Play(string fileName);
		void Stop();
	}
}
