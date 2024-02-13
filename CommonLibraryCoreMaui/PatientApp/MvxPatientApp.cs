using Acr.UserDialogs;
using CommonLibraryCoreMaui.PatientApp.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;


namespace CommonLibraryCoreMaui
{
    public class MvxPatientApp : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<PatientLoginViewModel>();

            Mvx.IoCProvider?.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
        }
    }

    //public class MvxPatientApp : Application
    //{
    //    //public App()
    //    //{
    //    //    InitializeComponent();

    //    //    MainPage = new AppShell();
    //    //}
    //    //public override void Initialize()
    //    //{
    //    //    CreatableTypes()
    //    //        .EndingWith("Service")
    //    //        .AsInterfaces()
    //    //        .RegisterAsLazySingleton();

    //    //    RegisterAppStart<PatientLoginViewModel>();

    //    //    Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
    //    //}
    //}
}