using Acr.UserDialogs;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class ManageSubscriptionPageViewModel : BaseViewModel
    {
        #region Constructor

        //To define the class level variable.
        string Token = string.Empty;
        int Userid = 0;
        public ManageSubscriptionPageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
            ChangePlanCommand = new Command(ChangePlanAsync);
            UpdatePaymentCommand = new Command(UpdatePaymentAsync);

            Token = Preferences.Get("AuthToken", string.Empty);
            Userid = Preferences.Get("UserId", 0);
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command ChangePlanCommand { get; set; }
        public Command UpdatePaymentCommand { get; set; }
        #endregion

        #region Properties
        #endregion

        #region Methods

        public async Task GetPatientSubscriptionInfo()
        {
            // Get App settings api..
            try
            {

                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    UserDialog.ShowLoading();
                    await Task.Run(async () =>
                    {
                        Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                        {
                            UserInfo userInfo = await DataUtility.GetUserInfo(SettingsValues.ApiURLValue, Userid, true, Token).ConfigureAwait(false);
                        });

                    }).ConfigureAwait(false);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await App.Current.MainPage.DisplayAlert("", "No Network Connection found, Please Connect to Internet first.", "OK");
                }
                UserDialog.HideLoading();
            }
            catch (Exception ex)
            {
                UserDialog.HideLoading();
            }
        }

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
        /// To Do: To define Change Plan command
        /// </summary>
        /// <param name="obj"></param>

        private async void ChangePlanAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyAccount.ChangePlanPage(), false);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// To Do: To define Update Payment command
        /// </summary>
        /// <param name="obj"></param>

        private async void UpdatePaymentAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyAccount.UpdateCardInformationPage(), false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion
    }
}
