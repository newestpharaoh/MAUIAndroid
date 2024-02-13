using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.NavigationParameters;
using CommonLibraryCoreMaui.ViewModels;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientMedicalInfoContactPCPResultsViewModel : BaseNavigationViewModel<PrimaryCareNavigationParam>, IMedicalIssueViewTitle
	{
        public IMvxMessenger _messenger;

		string firstName, lastName, state;
		private List<PCP> _primaryCare;
		public List<PCP> ListPrimaryCare
		{
			get { return _primaryCare; }
			set { SetProperty(ref _primaryCare, value); }
		}

		private PCP _selectedPrimaryCareMember;
		public PCP SelectedPrimaryCareMember
		{
			get { return _selectedPrimaryCareMember; }
			set { SetProperty(ref _selectedPrimaryCareMember, value); }
		}

		public IMvxCommand SelectPrimaryCareCommand => new MvxAsyncCommand(SelectPrimaryCareAsync);
		public IMvxCommand SelectOtherPrimaryCareCommand => new MvxAsyncCommand(SelectOtherPrimaryCareAsync);

        public MvvmCross.ViewModels.MvxInteraction NavigateBackToMedicalHistory { get; } = new MvvmCross.ViewModels.MvxInteraction();

        public string MedicalTitle { get; set; }

        public PatientMedicalInfoContactPCPResultsViewModel(IMvxMessenger messenger)
		{
            _messenger = messenger;
		}

        private async Task SelectPrimaryCareAsync()
        {
            if (SelectedPrimaryCareMember == null)
            {
                await _userDialogs.AlertAsync("Please select Primary Care Provider.");
                return;
            }

            NavigateBackToMedicalHistory.Raise();

          //  await _navigationService.Close(this, new Tuple<PCP, bool>(SelectedPrimaryCareMember, true));
            await _navigationService.Close(this);

            var message = new PCPMessage(this, SelectedPrimaryCareMember);
            _messenger.Publish(message);
        }

        private async Task SelectOtherPrimaryCareAsync()
		{
			//await _navigationService.Close(this, new Tuple<PCP, bool>(new PCP(), false));
            await _navigationService.Close(this);
        }

		private async Task Process()
		{
			try
			{
				State selectedState;

                if (Enum.TryParse(state, out selectedState))
                {
                    state = selectedState.GetDescription();
                }
				IsBusy = true;
				ListPrimaryCare = await DataUtility.SearchPCPAsync(SettingsValues.ApiURLValue, !string.IsNullOrEmpty(firstName) ? firstName : null, !string.IsNullOrEmpty(lastName) ? lastName : null, null, !string.IsNullOrEmpty(state) ? state : null);
				if (ListPrimaryCare.Count == 0)
				{
					ListPrimaryCare = null;
                    //IsBusy = false;
                    //_userDialogs.HideLoading();
                    return;
				}
			}
			catch { }
			IsBusy = false;
		}
		public override void ViewAppearing()
		{
			base.ViewAppearing();
            MvxNotifyTask.Create(Process);
           // _ = Task.Run(async () => { await Process(); });
		}

		public override void Prepare(PrimaryCareNavigationParam parameter)
		{
			firstName = parameter.FirstName;
			lastName = parameter.LastName;
			state = parameter.State;
			base.Prepare();

            MedicalTitle = $"{(parameter.NavigationParam.NavigationType == Models.NavigationParameters.MedicalInfoNavigationType.VisitHistoryPatient ? "Visit for " : "")}{parameter.NavigationParam.Name}";
		}
    }

    //move this to model after
    public enum State
    {
        [Description("Alabama")] AL,
        [Description("Alaska")] AK,
        [Description("Arizona")] AZ,
        [Description("Arkansas")] AR,
        [Description("California")] CA,
        [Description("Colorado")] CO,
        [Description("Connecticut")] CT,
        [Description("Delaware")] DE,
        [Description("Florida")] FL,
        [Description("Georgia")] GA,
        [Description("Hawaii")] HI,
        [Description("Idaho")] ID,
        [Description("Illinois")] IL,
        [Description("Indiana")] IN,
        [Description("Iowa")] IA,
        [Description("Kansas")] KS,
        [Description("Kentucky")] KY,
        [Description("Louisiana")] LA,
        [Description("Maine")] ME,
        [Description("Maryland")] MD,
        [Description("Massachusetts")] MA,
        [Description("Michigan")] MI,
        [Description("Minnesota")] MN,
        [Description("Mississippi")] MS,
        [Description("Missouri")] MO,
        [Description("Montana")] MT,
        [Description("Nebraska")] NE,
        [Description("Nevada")] NV,
        [Description("New Hampshire")] NH,
        [Description("New Jersey")] NJ,
        [Description("New Mexico")] NM,
        [Description("New York")] NY,
        [Description("North Carolina")] NC,
        [Description("North Dakota")] ND,
        [Description("Ohio")] OH,
        [Description("Oklahoma")] OK,
        [Description("Oregon")] OR,
        [Description("Pennsylvania")] PA,
        [Description("Rhode Island")] RI,
        [Description("South Carolina")] SC,
        [Description("South Dakota")] SD,
        [Description("Tennessee")] TN,
        [Description("Texas")] TX,
        [Description("Utah")] UT,
        [Description("Vermont")] VT,
        [Description("Virginia")] VA,
        [Description("Washington")] WA,
        [Description("West Virginia")] WV,
        [Description("Wisconsin")] WI,
        [Description("Wyoming")] WY
    }


    public static class EnumExtensions
    {
        public static string GetDescription(this State value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
    }

    public static class EnumEx
    {
        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", nameof(description));
        }
    }
}
