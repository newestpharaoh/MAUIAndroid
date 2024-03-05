using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class ChangePasswordSuccessPageViewModel : BaseViewModel
    {
        #region Constructor
        public ChangePasswordSuccessPageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
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
        #endregion
    }
}
