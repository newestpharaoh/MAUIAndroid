using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientRegistrationActivationEmailSentViewModel: BaseNavigationViewModel<string>
	{
		private string _emailText;
		public string EmailText
		{
			get { return _emailText; }
			set { SetProperty(ref _emailText, value); }
		}
        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }
        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }
        public IMvxCommand ContinueCommand => new MvxAsyncCommand(Continue);

		public PatientRegistrationActivationEmailSentViewModel(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}
        public async Task<List<CommonLibraryCoreMaui.Models.UIText>> GetUITopic(string strTopicName)
        {
            var pageText = await DataUtility.GetUITopicListAsync(SettingsValues.ApiURLValue, strTopicName,"en").ConfigureAwait(false);
            return pageText.UITextList;
        }
        private List<UIText> GetPageTextList()
        {
            Task<List<UIText>> task = Task.Run<List<UIText>>(async () => await GetUITopic("RegistrationVerification"));
            return task.Result;
        }
        public override void Prepare(string parameter)
		{
			Email = $" {MaskHelper.MaskEmail(parameter)} ";
            EmailText = $"An activation email has been sent to {MaskHelper.MaskEmail(parameter)}";
            Phone= $" {MaskHelper.MaskPhoneNumber(Registration.Instance.Phone)} ";

            base.Prepare();
		}

		private async Task Continue()
		{
			await _navigationService.Navigate<PatientLoginViewModel>();
		}
	}

	public class PatientRegistrationSingleMatchFoundViewModel : BaseViewModel
	{
		public IMvxCommand ContinueCommand => new MvxAsyncCommand(Continue);

		public PatientRegistrationSingleMatchFoundViewModel(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		private async Task Continue()
		{
			await _navigationService.Navigate<PatientLoginViewModel>();
		}
	}

	public class PatientRegistrationMultipleRecordsFoundViewModel : BaseViewModel { }

	public class PatientRegistrationNotPolicyHolderViewModel : BaseNavigationViewModel<PolicyType>
	{
		PolicyType type;
		private string _headingText;
		public string HeadingText
		{
			get { return _headingText; }
			set { SetProperty(ref _headingText, value); }
		}
		
  private string _informationText;
		public string InformationText
		{
			get { return _informationText; }
			set { SetProperty(ref _informationText, value); }
		}

		public override void Prepare(PolicyType parameter)
		{
			base.Prepare();
			type = parameter;
		}

		public override Task Initialize()
		{
			switch(type)
			{
				case PolicyType.NoPolicyHolder:
					HeadingText = "Not Policy Holder";
					InformationText = "Our records indicate that you are covered for this service under your primary account holders insurance. To use that coverage please ask the primary account holder to provide you access.  If you believe this information is in error please contact Customer Service";
					break;

				case PolicyType.NotCovered:
					HeadingText = "Not Covered";
					InformationText = "Our records indicate that you are not covered for this service. If you believe this information is in error please contact Customer Service at";
					break;

				case PolicyType.NotPrimaryAccount:
					HeadingText = "Not Primary Account Holder";
					InformationText = "Our records indicate that you are covered for this service under your primary account holders insurance. To use that coverage please ask the primary account holder to provide you access.\n\nIf you believe this information is in error please contact Customer service at";
					break;
			}

			return base.Initialize();
		}
	}

	public enum PolicyType
	{
		NotCovered,
		NotPrimaryAccount,
		NoPolicyHolder
	}
}
