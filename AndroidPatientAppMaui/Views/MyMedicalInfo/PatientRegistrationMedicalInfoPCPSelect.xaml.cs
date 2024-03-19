using Acr.UserDialogs;
using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoPCPSelect : ContentPage
{
    MyMedicalInfoDetailsPageViewModel VM;
    string FirstName = string.Empty;
    string LastName = string.Empty;
    string State = string.Empty;
    #region Constructor
    public PatientRegistrationMedicalInfoPCPSelect(string firstname, string lastname, string state, MyMedicalInfoDetailsPageViewModel pageViewModel)
    {
        try
        {
            InitializeComponent();
            VM = new MyMedicalInfoDetailsPageViewModel(this.Navigation);
            this.BindingContext = VM = pageViewModel;

            FirstName = firstname;
            LastName = lastname;
            State = state;
        }
        catch (Exception ex)
        {
        }
    }
    #endregion
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VM.DisplayPCPSelectDetails(FirstName, LastName, State);
    }

    private void listPrimaryCareProviders_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            var listView = sender as ListView;
            var selecteditem = e.SelectedItem as PCP;
            if (selecteditem != null)
            {
                VM.PcpSelectedItem = selecteditem;
                VM.lblPCPSelected = selecteditem.Preview;
            }
        }
        catch (Exception ex)
        { 
        }

    }

    private async void btnSelect_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (VM.PcpSelectedItem != null)
            {
                await Navigation.PushModalAsync(new Views.MyMedicalInfo.MyMedicalInfoDetailsPage(VM.UserName, VM.medicalInfo, VM.additionalFamilyMember, VM.PcpSelectedItem,null), false);
                //var intent = new Intent();
                //intent.PutExtraMedicalItem(selectedPCP);
                //SetResult(Result.Ok, intent);
                //Finish();
            }
            else
            {
                UserDialogs.Instance.Alert("Please select Primary Care Provider.");
            }
        }
        catch (Exception ex)
        {
        }
    }
}