using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MainTabs;

public partial class MainTabPage : TabbedPage
{
    string IsTabChange = string.Empty;
    string Token = string.Empty;
    int Userid = 0;
    public MainTabPage(string _IsTabChange)
    {
        try
        {
            InitializeComponent();
            IsTabChange = _IsTabChange;

            Token = Preferences.Get("AuthToken", string.Empty);
            Userid = Preferences.Get("UserId", 0);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    protected async override void OnAppearing()
    {
        try
        {

            base.OnAppearing();
            UserInfo userInfo = await DataUtility.GetUserInfo(SettingsValues.ApiURLValue, Userid, true, Token).ConfigureAwait(false);
            Helpers.AppGlobalConstants.userInfo = userInfo;


            if (IsTabChange == "Home")
                CurrentPage = Children[0];
            else if (IsTabChange == "VisitHistory")
                CurrentPage = Children[1];
            else if (IsTabChange == "FamilyAccount")
                CurrentPage = Children[2];
            else if (IsTabChange == "MyMedicalPage")
                CurrentPage = Children[3];
            else if (IsTabChange == "MyAccount")
                CurrentPage = Children[4];

            IsTabChange = string.Empty;
        }
        catch (Exception ex)
        {
             
        }
    }
}