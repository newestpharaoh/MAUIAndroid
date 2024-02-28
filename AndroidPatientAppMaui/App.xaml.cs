using AndroidPatientAppMaui.CustomControls;
using AndroidPatientAppMaui.Views;
using AndroidPatientAppMaui.Views.Account;
using AndroidPatientAppMaui.Views.Home;
using AndroidPatientAppMaui.Views.MainTabs;
using Microsoft.Maui.Controls.Platform;

namespace AndroidPatientAppMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Handlers.ModifyEntry();
            Handlers.ModifyPicker();
            Handlers.ModifyDatePicker();

            MainPage = new LoginPage();
            // MainPage = new Views.Home.HomePage();
        }
    }
}
