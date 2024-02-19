using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibraryCoreMaui;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
   public class UpdateCardInformationPageViewModel : BaseViewModel
    {
        #region Constructor
        public UpdateCardInformationPageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
        }
        #endregion

        #region Command
        public Command BackCommand { get; set; }
        #endregion

        
        #region Properties
        private List<string> _StatesList=  CommonLibraryCoreMaui.Theme.Values.States;
        public List<string>  StatesList
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

        private List<string> _CCYearsList = CommonLibraryCoreMaui.Theme.Values.CCYears;
        public List<string> CCYearsList
        {
            get { return _CCYearsList; }
            set
            {
                if (_CCYearsList != value)

                {
                    _CCYearsList = value;
                    OnPropertyChanged("CCYearsList");
                }
            }
        }

        private List<string> _MonthsNumericList = CommonLibraryCoreMaui.Theme.Values.MonthsNumeric;
        public List<string> MonthsNumericList
        {
            get { return _MonthsNumericList; }
            set
            {
                if (_MonthsNumericList != value)

                {
                    _MonthsNumericList = value;
                    OnPropertyChanged("MonthsNumericList");
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
        #endregion
    }
}
