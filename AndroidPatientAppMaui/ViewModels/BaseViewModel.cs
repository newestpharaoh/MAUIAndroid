using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidPatientAppMaui.BusinessCode;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace AndroidPatientAppMaui.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty, NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        public bool IsNotBusy => !IsBusy;

        private INavigation _Navigation;
        // private INavigationService navigationService; 

        public IBusinessCode _businessCode;
        //public IAlertService _alertService;
        //public ITelephoneService _telephoneService;
        //public IMediaService _mediaService;
        public Command BacksCommand { get; set; }
        public Command CloseCommand { get; set; }
        public INavigation Navigation
        {
            get { return _Navigation; }
            set
            {
                if (_Navigation != value)
                {
                    _Navigation = value;
                    OnPropertyChanged("Navigation");
                }
            }
        }

        public bool IsInitialized { get; set; }

        public BaseViewModel(INavigation navigation = null)
        {
            try
            {
                Navigation = navigation;
                BacksCommand = new Command(OnBacksAsync);
                CloseCommand = new Command(ClosePopupAsync);
                _businessCode = Ioc.Default.GetService<IBusinessCode>();
                //_alertService = Ioc.Default.GetService<IAlertService>();
                //_telephoneService = Ioc.Default.GetService<ITelephoneService>();
                //_mediaService = Ioc.Default.GetService<IMediaService>();

            }
            catch (Exception ex)
            { }
        }
        public void Busy() => IsBusy = true;
        public void NotBusy() => IsBusy = false;
        private void ClosePopupAsync(object obj)
        {
            this.PopAsync();
        }

        //public BaseViewModel(INavigationService navigationService)
        //{
        //    this.navigationService = navigationService;
        //}

        /// <summary>
        /// TODO : To Navigate To Back Page...
        /// </summary>
        public void OnBacksAsync()
        {
            PopModalAsync();
        }

        public Acr.UserDialogs.IUserDialogs UserDialog
        {
            get
            {
                return Acr.UserDialogs.UserDialogs.Instance;
            }
        }

        public async Task PushModalAsync(Page page)
        {
            if (Navigation != null)
                await Navigation.PushModalAsync(page);
        }

        public async Task PopModalAsync()
        {
            if (Navigation != null)
                await Navigation.PopModalAsync();
        }

        public async Task PushAsync(Page page)
        {
            if (Navigation != null)
                await Navigation.PushModalAsync(page);
        }
        public async Task PopAsync()
        {
            if (Navigation != null)
                await Navigation.PopModalAsync();
        }
    }
}
