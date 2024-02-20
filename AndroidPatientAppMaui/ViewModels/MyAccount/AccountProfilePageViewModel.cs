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
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command UpdateAccountAccessCommand { get; set; }
        public Command UpdateMedicalInformationCommand { get; set; }
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
                App.Current.MainPage = new Views.MyMedicalInfo.MyMedicalInfoPage();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// To Do: To define Update  command
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
        #endregion
    }


}
