using Android.Graphics.Drawables;
using AndroidPatientApp.CustomControls;
using AndroidPatientApp.Views;
using Microsoft.Maui.Controls.Platform;

namespace AndroidPatientApp
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
