using Acr.UserDialogs;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyMedicalInfo
{
    public class PatientRegistrationMedicalInfoAllergyViewModel : BaseViewModel
    {
        // To Define local Class Level variable
        Allergy allergy;

        #region Constructor
        public PatientRegistrationMedicalInfoAllergyViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);
               // AllergySaveCommand = new Command(AllergySaveAsync);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command SaveCommand { get; set; }
        #endregion

        #region Properties

        private string _UserName = Helpers.AppGlobalConstants.userInfo.Name;
      

        #endregion

        #region Methods
        /// <summary>
        /// ToDo: To define the back command
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

        public async Task DisplayDeails()
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
                            //if (allergy != null)
                            //{
                            //    txtAllergy = allergy.Name;
                            //    txtComments = allergy.Description;
                            //    lblHeading = "Edit Allergy";
                            //}

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
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// ToDo: To define the Save command
        /// </summary>
        /// <param name="obj"></param>

        private async void SaveAsync(object obj)
        {
            try
         {
            //    if (!string.IsNullOrEmpty(txtAllergy.Trim()))
            //    { 
            //        if (allergy != null)
            //        {
            //            allergy.Name = txtAllergy;
            //            allergy.Description = txtComments;
            //           // intent.PutExtraMedicalItem(allergy);
            //        }
            //        else
            //        {
            //            //intent.PutExtraMedicalItem(new Allergy() { Name = txtAllergy.Text, Description = txtComments.Text });
            //        }
            //        await Navigation.PopModalAsync();
            //    }
            //    else
            //    {
            //        UserDialog.Alert("Please fill all the required fields!");
            //    }
            }
            catch (Exception ex)
            { 
            }
        }

        #endregion
    }
}
