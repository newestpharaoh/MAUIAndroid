using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class UpdateAccountAccessPageViewModel :BaseViewModel
    {
        #region Constructor
        public UpdateAccountAccessPageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
            PrivatizeUserCommand = new Command(PrivatizeUserAsync);
            DeactivateNowCommand = new Command(DeactivateNowAsync);
            RemoveUserCommand = new Command(RemoveUserAsync);
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command PrivatizeUserCommand { get; set; }
        public Command DeactivateNowCommand { get; set; }
        public Command RemoveUserCommand { get; set; }
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
        /// To Do: To define Deactivate command
        /// </summary>
        /// <param name="obj"></param>

        private async void DeactivateNowAsync(object obj)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// To Do: To define Privatize User command
        /// </summary>
        /// <param name="obj"></param>

        private async void PrivatizeUserAsync(object obj)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// To Do: To define Privatize User command
        /// </summary>
        /// <param name="obj"></param>

        private async void RemoveUserAsync(object obj)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion
    }
}
