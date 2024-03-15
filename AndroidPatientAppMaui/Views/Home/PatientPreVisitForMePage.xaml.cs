using AndroidPatientAppMaui.ViewModels.Home;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.Home;

public partial class PatientPreVisitForMePage : ContentPage
{
    PatientPreVisitForMePageViewModel VM;
    public AccountMember am;
    public PatientPreVisitForMePage()
    {
        try
        {

            InitializeComponent();
            this.BindingContext = VM = new PatientPreVisitForMePageViewModel(this.Navigation);
        }
        catch (Exception ex)
        {
        }
    }

    private void CustomPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //VM.lblMemberName = string.Empty;
            //if (VM.patientProfile != null)
            //{
            //    var item = sender as Picker;
            //    VM.patientProfile.Gender = item.SelectedItem.ToString();
            //}
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}