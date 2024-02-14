using AndroidPatientAppMaui.CustomControls;
using AndroidPatientAppMaui.Views;
using AndroidPatientAppMaui.Views.Account;
using AndroidPatientAppMaui.Views.Home;
using Microsoft.Maui.Controls.Platform;

namespace AndroidPatientAppMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Handlers.ModifyEntry();
            Handlers.ModifyDatePicker();

            //MainPage = new AppShell();
            MainPage = new LoginPage();
        }
    }
}
