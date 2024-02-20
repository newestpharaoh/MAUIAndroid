using Acr.UserDialogs;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyMedicalInfo
{
    public class MyMedicalInfoPageViewModel : BaseViewModel
    {
        #region Constructor
        //To define the class level variable.
        string Token = string.Empty;

        public MyMedicalInfoPageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);

            Token = Preferences.Get("AuthToken", string.Empty);
        }
        #endregion

        #region Command

        public Command BackCommand { get; set; }
        #endregion

        #region Properties
        #endregion

        #region Methods
        /// <summary>
        /// To define the back button command.
        /// </summary>
        /// <param name="obj"></param>
        private async void BackAsync(object obj)
        {
            try
            {
                await Navigation.PopModalAsync();
            }
            catch (Exception ex) { }
        }
        public async Task DisplayMedicalInfo(int patientId)
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
                            MedicalInfo medicalInfo = await DataUtility.PatientGetMedicalHistoryAsync(SettingsValues.ApiURLValue, Token, patientId).ConfigureAwait(false);
                            List<MedicalIssue> issues = await DataUtility.GetMedicalIssuesAsync(SettingsValues.ApiURLValue).ConfigureAwait(false);
                            if (medicalInfo != null)
                            {
                                foreach (Medication medication in medicalInfo.Medications)
                                {
                                    var a = medication.Name;
                                }
                            }
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
        #endregion
    }
}
