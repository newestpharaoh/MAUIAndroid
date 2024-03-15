using Acr.UserDialogs;
using AndroidPatientAppMaui.Views;
using CommonLibraryCoreMaui.Models;
using CommunityToolkit.Maui.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class MyAccountPageViewModel :BaseViewModel
    {
        #region Constructor
        public MyAccountPageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);
                FinePrintCommand = new Command(FinePrintAsync);
                SignOutCommand = new Command(SignOutAsync);
                ManageSubscriptionCommand = new Command(ManageSubscriptionAsync);
                AccountProfilesCommand = new Command(AccountProfilesAsync);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command FinePrintCommand { get; set; }
        public Command SignOutCommand { get; set; }
        public Command ManageSubscriptionCommand { get; set; }
        public Command AccountProfilesCommand { get; set; }
        #endregion

        #region Properties
        #endregion

        #region Methods
        /// <summary>
        /// To Do: To define back command
        /// </summary>
        /// <param name="obj"></param> 
        private async void BackAsync(object obj)
        {
            try
            {
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// To Do: To define Sign Out command
        /// </summary>
        /// <param name="obj"></param>
        private async void SignOutAsync(object obj)
        {
            try
            {
                bool answer = await App.Current.MainPage.DisplayAlert("Sign Out", "Are you sure you want to sign out?", "Yes", "No");
                if (answer)
                {
                    AndroidPatientAppMaui.Helpers.AppGlobalConstants.Token = "";
                    AndroidPatientAppMaui.Helpers.AppGlobalConstants.UserId = 0;
                    AndroidPatientAppMaui.Helpers.AppGlobalConstants.TokenExpirationDate = DateTime.Now;
                    AndroidPatientAppMaui.Helpers.AppGlobalConstants.LoginEmail = "";
                    AndroidPatientAppMaui.Helpers.AppGlobalConstants.userInfo = new UserInfo();
                    Preferences.Set("AuthToken", "");
                    Preferences.Set("UserId", 0);
                    Preferences.Set("PatientID", 0);
                    await Navigation.PushModalAsync(new LoginPage(), false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// To Do: To define Account Profiles command
        /// </summary>
        /// <param name="obj"></param>
        private async void AccountProfilesAsync(object obj)
        {
            try
            {
                //App.Current.MainPage = new Views.MainTabs.MainTabPage();
                //  await Navigation.PushModalAsync(new Views.MyAccount.AccountProfilePage(), false);
               // App.Current.MainPage = new Views.MainTabs.MainTabPage("FamilyAccount");
                App.Current.MainPage = new Views.MyAccount.AccountProfilePage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// To Do: To define Fine Print command
        /// </summary>
        /// <param name="obj"></param>
        private async void FinePrintAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyAccount.FinePrintPage(), false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// To Do: To define Manage Subscription command
        /// </summary>
        /// <param name="obj"></param>
        private async void ManageSubscriptionAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyAccount.ManageSubscriptionPage(), false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion
    }
}
