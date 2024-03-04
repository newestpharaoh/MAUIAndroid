using AndroidPatientAppMaui.BusinessCode;
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
        //TODO : To Define Global Variables Here....
        private static Autofac.IContainer _container;
        public static IBusinessCode BusinessCode;
        public App()
        {
            InitializeComponent();

            //To initialize Containers..
            AppSetup appSetup = new AppSetup();
            _container = appSetup.CreateContainer();

            Handlers.ModifyEntry();
            Handlers.ModifyPicker();
            Handlers.ModifyDatePicker();

            MainPage = new LoginPage();
            // MainPage = new Views.Home.HomePage();
        }
    }
}
