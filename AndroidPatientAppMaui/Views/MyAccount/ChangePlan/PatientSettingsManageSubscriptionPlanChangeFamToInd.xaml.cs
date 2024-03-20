using AndroidPatientAppMaui.ViewModels.MyAccount.ChangePlan;

namespace AndroidPatientAppMaui.Views.MyAccount.ChangePlan;

public partial class PatientSettingsManageSubscriptionPlanChangeFamToInd : ContentPage
{
    // To Define Local Class variable
    PatientSettingsManageSubscriptionPlanChangeFamToIndViewModel VM;

    #region Constrcutor
    public PatientSettingsManageSubscriptionPlanChangeFamToInd()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new PatientSettingsManageSubscriptionPlanChangeFamToIndViewModel(this.Navigation);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    #endregion

    #region Event Hanlder


    protected override void OnAppearing()
    {
        base.OnAppearing();
        VM.GetChangeMembers();
    }

    /// <summary>
    ///  ToDo : To Define Info Clicked Event 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Info_Clicked(object sender, TappedEventArgs e)
    {

    }
    #endregion
}