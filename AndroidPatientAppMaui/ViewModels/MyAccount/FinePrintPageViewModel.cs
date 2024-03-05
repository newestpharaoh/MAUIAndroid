using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class FinePrintPageViewModel : BaseViewModel
    {
        #region Constructor
        public FinePrintPageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);
                TermsOfUseCommand = new Command(TermsOfUseAsync);
                BillingPoliciesCommand = new Command(BillingPoliciesAsync);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command TermsOfUseCommand { get; set; }
        public Command BillingPoliciesCommand { get; set; }
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
        /// To Do: To define TermsOfUse command
        /// </summary>
        /// <param name="obj"></param>

        private async void TermsOfUseAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyAccount.TermsOfUsePage(), false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// To Do: To define BillingPolicies command
        /// </summary>
        /// <param name="obj"></param>

        private async void BillingPoliciesAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyAccount.BillingPoliciesPage(), false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion
    }
}
