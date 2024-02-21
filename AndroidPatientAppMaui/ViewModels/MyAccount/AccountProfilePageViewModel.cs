using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class AccountProfilePageViewModel :BaseViewModel
    {
        #region Constructor
        public AccountProfilePageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
            UpdateAccountAccessCommand = new Command(UpdateAccountAccessAsync);
            UpdateMedicalInformationCommand = new Command(UpdateMedicalInformationAsync);
            UpdateDemographicsCommand = new Command(UpdateDemographicsAsync);
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command UpdateAccountAccessCommand { get; set; }
        public Command UpdateMedicalInformationCommand { get; set; }
        public Command UpdateDemographicsCommand { get; set; }
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
        /// To Do: To define Update Medical Info command
        /// </summary>
        /// <param name="obj"></param>

        private async void UpdateMedicalInformationAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyMedicalInfo.MyMedicalInfoPage(), false);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// To Do: To define Update Account Access command
        /// </summary>
        /// <param name="obj"></param>

        private async void UpdateAccountAccessAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyAccount.UpdateAccountAccessPage(), false);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        /// <summary>
        /// To Do: To define Update Demographics command
        /// </summary>
        /// <param name="obj"></param>

        private async void UpdateDemographicsAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyAccount.UpdateDemographicsPage(), false);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion
    }


}
