using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class MyAccountPage : ContentPage
{
    //To define the class lavel variable.
    MyAccountPageViewModel VM;
    #region Constructor
    public MyAccountPage()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new MyAccountPageViewModel(this.Navigation);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion

    #region Event Handler
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
    #endregion

}