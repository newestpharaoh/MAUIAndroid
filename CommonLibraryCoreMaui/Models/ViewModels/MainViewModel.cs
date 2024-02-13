using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CommonLibraryCoreMaui.Models.ViewModels
{
    public class MainViewModel
    {
        protected IAppInfo AppInfo { get; }
        public MainViewModel()
        {
            // Xamarin.Forms doesn't support constructor injection
            // out of the box. We will manually resolve IAppInfo. There
            // are other options that go outside the scope of this article
            // for constructor injection. Scroll down to further reading
            AppInfo = DependencyService.Get<IAppInfo>();
        }

        public string Name { get => AppInfo.AppTitle; }

        // public string Version { get => AppInfo.Version; }
    }

}
