using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.Home
{
    public class HomePageViewModel : BaseViewModel
    {
        #region Constructor
        public HomePageViewModel(INavigation nav)
        {
            Navigation = nav;
            SignOutCommand = new Command(SignOutAsync);    
        }
        #endregion

        #region Command
        public Command SignOutCommand { get; set; }
        #endregion

        #region Properties

        private string _phoneNumber = "512-421-5678";
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged("PhoneNumber");
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// To Do: To define Sign Out command
        /// </summary>
        /// <param name="obj"></param>

        private async void SignOutAsync(object obj)
        {
            try
            {
                await App.Current.MainPage.DisplayAlert("Sign Out", "Are you sure you want to sign out?", "Yes", "No");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion

    }
}
