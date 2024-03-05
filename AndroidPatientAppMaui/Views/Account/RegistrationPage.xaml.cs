using AndroidPatientAppMaui.CustomControls;
using AndroidPatientAppMaui.ViewModels;
using AndroidPatientAppMaui.ViewModels.Account;
using Microsoft.Maui.Controls.Platform;

namespace AndroidPatientAppMaui.Views;

public partial class RegistrationPage : ContentPage
{

    //TODO : To Define Local Class Level Variables...
   RegistrationPageViewModel VM;

    #region Constructor
    public RegistrationPage()
	{
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new RegistrationPageViewModel(this.Navigation);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        } 
    }
    #endregion

    #region Event Handler

    #endregion
}