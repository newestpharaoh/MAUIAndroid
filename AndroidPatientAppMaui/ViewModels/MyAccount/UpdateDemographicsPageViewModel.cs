using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class UpdateDemographicsPageViewModel :BaseViewModel
    {
        #region Constructor
        public UpdateDemographicsPageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
            ChangeProfilePhotoCommand = new Command(ChangeProfilePhotoAsync);
            ChangePasswordCommand = new Command(ChangePasswordAsync);
            SaveChangesCommand = new Command(SaveChangesAsync);
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        public Command ChangeProfilePhotoCommand { get; set; }
        public Command ChangePasswordCommand { get; set; }
        public Command SaveChangesCommand { get; set; }
        #endregion

        #region Properties

        private List<string> _UserTitlesList = CommonLibraryCoreMaui.Theme.Values.UserTitles;
        public List<string> UserTitlesList
        {
            get { return _UserTitlesList; }
            set
            {
                if (_UserTitlesList != value)

                {
                    _UserTitlesList = value;
                    OnPropertyChanged("UserTitlesList");
                }
            }
        }

        private List<string> _GenderOptionsList = CommonLibraryCoreMaui.Theme.Values.GenderOptions;
        public List<string> GenderOptionsList
        {
            get { return _GenderOptionsList; }
            set
            {
                if (_GenderOptionsList != value)

                {
                    _GenderOptionsList = value;
                    OnPropertyChanged("GenderOptionsList");
                }
            }
        }

        private List<string> _StatesList = CommonLibraryCoreMaui.Theme.Values.States;
        public List<string> StatesList
        {
            get { return _StatesList; }
            set
            {
                if (_StatesList != value)

                {
                    _StatesList = value;
                    OnPropertyChanged("StatesList");
                }
            }
        }

        private List<string> _LanguagesList = CommonLibraryCoreMaui.Theme.Values.Languages;
        public List<string> LanguagesList
        {
            get { return _LanguagesList; }
            set
            {
                if (_LanguagesList != value)

                {
                    _LanguagesList = value;
                    OnPropertyChanged("LanguagesList");
                }
            }
        }
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
        /// To Do: To define Change Profile command
        /// </summary>
        /// <param name="obj"></param>

        private async void ChangeProfilePhotoAsync(object obj)
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
        /// To Do: To define  Change Password command
        /// </summary>
        /// <param name="obj"></param>

        private async void ChangePasswordAsync(object obj)
        {
            try
            {
                await Navigation.PushModalAsync(new Views.MyAccount.ChangePasswordPage(), false);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// To Do: To define Save Changes command
        /// </summary>
        /// <param name="obj"></param>
        private async void SaveChangesAsync(object obj)
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
