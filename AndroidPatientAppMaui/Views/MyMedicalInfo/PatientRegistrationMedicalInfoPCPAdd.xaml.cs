using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoPCPAdd : ContentPage
{
    MyMedicalInfoDetailsPageViewModel VM;
    #region Constructor
    public PatientRegistrationMedicalInfoPCPAdd(PCP pcp, int code, MyMedicalInfoDetailsPageViewModel pageViewModel)
    {
        InitializeComponent();
        this.BindingContext = VM = pageViewModel;

        VM.pcp = pcp;
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
            VM.StatePCPLbl = string.Empty;

            Picker picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                //  VM.pharmacy.State = (string)picker.SelectedItem;
                VM.StatePCPLbl = (string)picker.SelectedItem;

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }
    #endregion 
}