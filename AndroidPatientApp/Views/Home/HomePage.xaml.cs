using AndroidPatientApp.ViewModels.Home;

namespace AndroidPatientApp.Views.Home;

public partial class HomePage : ContentPage
{
    HomePageViewModel VM;
    public HomePage()
    {
        InitializeComponent();
        this.BindingContext = VM = new HomePageViewModel(this.Navigation);
    }

    #region Event Handler
    #endregion
}