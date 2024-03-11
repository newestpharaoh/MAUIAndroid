using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoPCPSearch : ContentPage
{
    MyMedicalInfoDetailsPageViewModel VM;
    #region Constructor
    public PatientRegistrationMedicalInfoPCPSearch(PCP pCP, int code, MyMedicalInfoDetailsPageViewModel pageViewModel)
    {
        InitializeComponent();
        this.BindingContext = VM = pageViewModel;
    }
    #endregion

    #region Event Handler

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VM.DisplayPCPDetails();
    }
    private void StatePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            VM.StatePCPSearchLbl = string.Empty;

            Picker picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                //  VM.pharmacy.State = (string)picker.SelectedItem;
                VM.StatePCPSearchLbl = (string)picker.SelectedItem;

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }
    #endregion
}