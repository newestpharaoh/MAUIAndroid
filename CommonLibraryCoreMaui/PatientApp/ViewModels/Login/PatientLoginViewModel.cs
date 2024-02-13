using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.ViewModels;
using SimpleInjector;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientLoginViewModel : BaseViewModel
    {
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

		private bool _isErorr = false;
		public bool IsErorr
		{
			get { return _isErorr; }
			set { SetProperty(ref _isErorr, value); }
		}

		private string _errorText;
		public string ErrorText
		{
			get { return _errorText; }
			set { SetProperty(ref _errorText, value); }
		}

        private string _appBrandName;
        public string AppBrandName
        {
            get { return _appBrandName; }
            set { SetProperty(ref _appBrandName, value); }
        }
        public IMvxCommand LoginCommand => new MvxAsyncCommand(Login);
        public IMvxCommand RegisterCommand => new MvxAsyncCommand(Register);
		public IMvxCommand ForgotPasswordCommand => new MvxAsyncCommand(ForgotPassword);

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            UserName = string.Empty;
            Password = string.Empty;
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

		private async Task ForgotPassword()
		{
			IsErorr = false;
			await _navigationService.Navigate<ForgotPasswordUserInfoViewModel>();
        }

		public override void ViewDisappeared()
		{
			base.ViewDisappeared();
			IsErorr = false;
		}

		private async Task Login()
        {
#if DEBUG
          UserName  = "scchauhan@austinregionalclinic.com";//"masepeda@austinregionalclinic.com";//"testregistration917@testmail.com"; // "scchauhan@austinregionalclinic.com";//"testregistration912@testmail.com";
            Password = "1ChangeMe!"; //"Active!23"; //"1ChangeMe!";//
            //UserName = "testregistration1011@testmail.com";// "scchauhan@austinregionalclinic.com"; // "scchauhan@austinregionalclinic.com";//"testregistration912@testmail.com";
            //Password = "Active!23"; //"1ChangeMe!";//
            //UserName = "ashtami.mata@testmail.com"; //"testregistration571@testmail.com";////(For Curative)
            //Password = "Active!23"; //"Active!
            // UserName = "testregistration1086@testmail.com";//"testregistration1038@testmail.com";//(Fam) "testregistration1038@testmail.com@testmail.com"; //"testregistration571@testmail.com";////(For Curative)
           // Password = "Active!23"; //"Active!
#endif
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
				IsErorr = false;
				IsBusy = true;
                var resp = await DataUtility.GetTokenResponseAsync(SettingsValues.ApiURLValue, UserName, Password, "0");

                if (resp != null)
                {
                    if (!string.IsNullOrEmpty(resp.error))
                    {
                        if (resp.error.ToLower().Equals("invalid credentials"))
                        {
                            ErrorText = "Login failed. Invalid credentials.";
                        }
                        else
                        {
                            ErrorText = "Information incorrect. Please review and try again.";
                        }

                        IsErorr = true;
                        IsBusy = false;
                    }
                    else
                    {
                        CommonAuthSession.Token = resp.access_token;
                        CommonAuthSession.SetTokenExpirationDate(DateTime.Now.AddSeconds(Convert.ToInt32(resp.expires_in)));

                        UserInfo user = await DataUtility.GetUserInfo(SettingsValues.ApiURLValue, resp.userid, true, resp.access_token).ConfigureAwait(false);
                        Globals.Instance.UserInfo = user;
						Globals.Instance.UserInfo.ProviderID = null;
                        Globals.Instance.IsTermed = user.IsTermed;

                        CommonAuthSession.FirstName = user.FirstName;
                        CommonAuthSession.LastName = user.LastName;
                      
                        Globals.Instance.IsCurative = user.Domain != null && user.Domain.ToLower() == "curative" ? true : false;
                        Globals.Instance.IsCapsuleVisible = Globals.Instance.IsCurative ? false : true;

                        var container = new Container();
                        container.Register<IiOSLogout, Services.PatientLogoutService>();
                       // container.Verify();
                        DependencyResolver.SetupContainer(container);
#if DEBUG
                        if (Globals.Instance.UserInfo.ProviderID is null)
                        {
                            UserInfo userInfo = await DataUtility.GetUserInfo(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.UserID, true, CommonAuthSession.Token).ConfigureAwait(false);
                            Globals.Instance.UserInfo = userInfo;
                        }
                        CommonAuthSession.IsAutheticated = true;
                        await _navigationService.Navigate<HomeViewModel>();
#else
                        await _navigationService.Navigate<PatientTwoFactorAuthViewModel>();                         
                        IsBusy = false;
#endif
                    }
                }
                else
				{
                    ErrorText = "Information incorrect. Please review and try again.";
                    IsErorr = true;
                    IsBusy = false;
                }
            }
            else
            {
				IsErorr = true;
				ErrorText = "Please enter your email and password.";
                IsBusy = false;
            }
			IsBusy = false;
		}

        private async Task Register()
        {
			IsErorr = false;
			Models.Registration.Instance.Clear();
			Models.Registration.Instance.IsSelfPay = true;
			await _navigationService.Navigate<PatientPreRegistrationViewModel>();
        }

        public async Task<bool> CheckAppVersionAsync(string appVersion)
        {
            return await DataUtility.IsAppVersionLessThanRecommendation(SettingsValues.ApiURLValue, appVersion, "iOS Patient").ConfigureAwait(false);
        }

        public async Task<SiteSettings> GetSiteSettingAppName()
        {
            return await DataUtility.GetSiteSettingsAsync(SettingsValues.ApiURLValue).ConfigureAwait(false);             
        }
    }

	public class PatientNewAppNoticeViewModel : BaseViewModel
	{

	}
}
