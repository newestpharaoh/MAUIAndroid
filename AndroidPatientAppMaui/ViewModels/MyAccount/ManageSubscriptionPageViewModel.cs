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
        public ManageSubscriptionPageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
            ChangePlanCommand = new Command(ChangePlanAsync);
            UpdatePaymentCommand = new Command(UpdatePaymentAsync);
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
