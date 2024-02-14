using Acr.UserDialogs;
using CommunityToolkit.Maui.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class MyAccountPageViewModel :BaseViewModel
    {
        #region Constructor
        public MyAccountPageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
            FinePrintCommand = new Command(FinePrintAsync);
            SignOutCommand = new Command(SignOutAsync);
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command FinePrintCommand { get; set; }
        public Command SignOutCommand { get; set; }
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
        /// To Do: To define Fine Print command
        /// </summary>
        /// <param name="obj"></param>

        private async void SignOutAsync(object obj)
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
        /// To Do: To define Sign Out command
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
        #endregion
    }
}
