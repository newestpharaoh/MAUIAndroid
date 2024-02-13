using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientPreVisitPatientSelectionStep1ViewModel : BaseViewModel
    {
        public IMvxCommand ContinueCommand => new MvxAsyncCommand(Continue);

        private VisitForViewModel _visitFor;
        public VisitForViewModel VisitFor
        {
            get { return _visitFor; }
            set { SetProperty(ref _visitFor, value); }
        }

        private bool _readNoticeCheck;
        public bool ReadNoticeCheck
        {
            get { return _readNoticeCheck; }
            set {
                SetProperty(ref _readNoticeCheck, value);
            }
        }

        public PatientPreVisitPatientSelectionStep1ViewModel()
        {
        }

        public async override Task Initialize()
        {
            try
            {
                VisitFor = new VisitForViewModel() { VisitFor = PatientPreVisitPatientSelectionStep1.Me };
                ReadNoticeCheck = false;
            }
            catch { }
            await base.Initialize();
        }

        private async Task Continue()
        {
            if (VisitFor.VisitFor == PatientPreVisitPatientSelectionStep1.Me)
            {
                await _navigationService.Navigate<PatientPreVisitPatientSelectionIndividualViewModel>();
            }
            else
            {
                await _navigationService.Navigate<PatientPreVisitPatientSelectionFamilyViewModel>();
            }
        }
    }

    public enum PatientPreVisitPatientSelectionStep1
    {
        Me = 0,
        SomeoneElse = 1
    }

    public class VisitForViewModel : MvvmCross.ViewModels.MvxViewModel
    {
        private PatientPreVisitPatientSelectionStep1 _visitFor;
        public PatientPreVisitPatientSelectionStep1 VisitFor
        {
            get { return _visitFor; }
            set { SetProperty(ref _visitFor, value); }
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }
    }

    public enum RadioSelectedOption
    {
        First = 0,
        Second = 1
    }

    public class RadioSelectionViewModel : MvvmCross.ViewModels.MvxViewModel
    {
        private RadioSelectedOption _selectedOption;
        public RadioSelectedOption SelectedOption
        {
            get { return _selectedOption; }
            set { SetProperty(ref _selectedOption, value); }
        }
    }
}
