using System;

namespace CommonLibraryCoreMaui
{
    public interface IDialogFragment
    {
        EventArgs GetEventArgs();
        string Validate();
    }

    public interface IAppInfo
    {
        string AppTitle { get; }
        //string Version { get; }
    }
}
