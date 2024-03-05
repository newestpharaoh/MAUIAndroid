using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class ProviderSelectionPage : ContentPage
{
    ProviderSelectionPageViewModel VM;
    #region Constructor
    public ProviderSelectionPage(string PatientName)
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new ProviderSelectionPageViewModel(this.Navigation);
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