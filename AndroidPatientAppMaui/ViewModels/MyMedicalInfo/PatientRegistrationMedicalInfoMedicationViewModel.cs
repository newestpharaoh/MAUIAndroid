using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyMedicalInfo
{
    public class PatientRegistrationMedicalInfoMedicationViewModel : BaseViewModel
    {   
        // To Define local Class Level variable

        #region Constructor
        public PatientRegistrationMedicalInfoMedicationViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);
                SaveCommand = new Command(SaveAsync);
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
        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }
        private string _txtFirstName;
        public string txtFirstName
        {
            get { return _txtFirstName; }
            set
            {
                if (_txtFirstName != value)
                {
                    _txtFirstName = value;
                    OnPropertyChanged("txtFirstName");
                }
            }
        }

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

        /// <summary>
        /// ToDo: To define the Save command
        /// </summary>
        /// <param name="obj"></param>

        private async void SaveAsync(object obj)
        {

        }

        #endregion

    }
}
