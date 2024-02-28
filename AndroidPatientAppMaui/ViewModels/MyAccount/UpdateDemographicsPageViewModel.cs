using Acr.UserDialogs;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models; 
using System.Globalization;
using System.Text.RegularExpressions;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class UpdateDemographicsPageViewModel :BaseViewModel
    {
        //To define the class level variable.
       public BasicFamilyMemberInfo member;
        PatientProfile patientProfile;
        int PatientID = 0;
        string Token = string.Empty;
        string[] titleItems;
        #region Constructor
        public UpdateDemographicsPageViewModel(INavigation nav)
        {
            Navigation = nav;
            BackCommand = new Command(BackAsync);
            ChangeProfilePhotoCommand = new Command(ChangeProfilePhotoAsync);
            ChangePasswordCommand = new Command(ChangePasswordAsync);
            SaveChangesCommand = new Command(SaveChangesAsync);

            Token = Preferences.Get("AuthToken", string.Empty);
            PatientID = Preferences.Get("PatientID", 0);
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
        private List<CommonLibraryCoreMaui.Models.GenericRecord> _RelationshipList = CommonLibraryCoreMaui.Theme.Values.Relationships;
        public List<CommonLibraryCoreMaui.Models.GenericRecord> RelationshipList
        {
            get { return _RelationshipList; }
            set
            {
                if (_RelationshipList != value)

                {
                    _RelationshipList = value;
                    OnPropertyChanged("RelationshipList");
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

        FileResult _mediafileResult;
        public FileResult MediafileResult
        {
            get
            {
                return _mediafileResult;
            }
            set
            {
                _mediafileResult = value;
                OnPropertyChanged("MediafileResult");
            }
        }


        private string _ProfileImage;
        public string ProfileImage
        {
            get { return _ProfileImage; }
            set
            {
                if (_ProfileImage != value)
                {
                    _ProfileImage = value;
                    OnPropertyChanged("ProfileImage");
                }
            }
        }
        private string _UserName;
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
        private bool _lytNotificationPreferences = true;
        public bool lytNotificationPreferences
        {
            get { return _lytNotificationPreferences; }
            set
            {
                if (_lytNotificationPreferences != value)
                {
                    _lytNotificationPreferences = value;
                    OnPropertyChanged("lytNotificationPreferences");
                }
            }
        }
        private bool _rbtnTextNotification = false;
        public bool rbtnTextNotification
        {
            get { return _rbtnTextNotification; }
            set
            {
                if (_rbtnTextNotification != value)
                {
                    _rbtnTextNotification = value;
                    OnPropertyChanged("rbtnTextNotification");
                }
            }
        }
        private bool _rbtnEmailNotification = false;
        public bool rbtnEmailNotification
        {
            get { return _rbtnEmailNotification; }
            set
            {
                if (_rbtnEmailNotification != value)
                {
                    _rbtnEmailNotification = value;
                    OnPropertyChanged("rbtnEmailNotification");
                }
            }
        }
        private bool _lytRelationship = false;
        public bool lytRelationship
        {
            get { return _lytRelationship; }
            set
            {
                if (_lytRelationship != value)
                {
                    _lytRelationship = value;
                    OnPropertyChanged("lytRelationship");
                }
            }
        }
        private bool _lytOtherRelationship = false;
        public bool lytOtherRelationship
        {
            get { return _lytOtherRelationship; }
            set
            {
                if (_lytOtherRelationship != value)
                {
                    _lytOtherRelationship = value;
                    OnPropertyChanged("lytOtherRelationship");
                }
            }
        }
        private string _txtOtherRelationship;
        public string txtOtherRelationship
        {
            get { return _txtOtherRelationship; }
            set
            {
                if (_txtOtherRelationship != value)
                {
                    _txtOtherRelationship = value;
                    OnPropertyChanged("txtOtherRelationship");
                }
            }
        } 
        private string _resultEmail;
        public string ResultEmail
        {
            get { return _resultEmail; }
            set
            {
                if (_resultEmail != value)
                {
                    _resultEmail = value;
                    OnPropertyChanged("ResultEmail");
                }
            }
        } 
        private string _resultPhonenumber;
        public string ResultPhonenumber
        {
            get { return _resultPhonenumber; }
            set
            {
                if (_resultPhonenumber != value)
                {
                    _resultPhonenumber = value;
                    OnPropertyChanged("ResultPhonenumber");
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
        private string _txtMiddleName;
        public string txtMiddleName
        {
            get { return _txtMiddleName; }
            set
            {
                if (_txtMiddleName != value)
                {
                    _txtMiddleName = value;
                    OnPropertyChanged("txtMiddleName");
                }
            }
        } 
        private string _txtLastName;
        public string txtLastName
        {
            get { return _txtLastName; }
            set
            {
                if (_txtLastName != value)
                {
                    _txtLastName = value;
                    OnPropertyChanged("txtLastName");
                }
            }
        } 
        private string _txtPreferredName;
        public string txtPreferredName
        {
            get { return _txtPreferredName; }
            set
            {
                if (_txtPreferredName != value)
                {
                    _txtPreferredName = value;
                    OnPropertyChanged("txtPreferredName");
                }
            }
        }
        private DateTime _txtDOB;
        public DateTime txtDOB
        {
            get => _txtDOB;
            set
            {
                if (_txtDOB != value)
                {
                    _txtDOB = value;
                    OnPropertyChanged(nameof(txtDOB));
                }
            }
        }
        private string _spnrGender;
        public string spnrGender
        {
            get => _spnrGender;
            set
            {
                if (_spnrGender != value)
                {
                    _spnrGender = value;
                    OnPropertyChanged(nameof(spnrGender));
                }
            }
        }
        private string _spnrLanguage;
        public string spnrLanguage
        {
            get => _spnrLanguage;
            set
            {
                if (_spnrLanguage != value)
                {
                    _spnrLanguage = value;
                    OnPropertyChanged(nameof(spnrLanguage));
                }
            }
        }
        private string _Email;
        public string Email
        {
            get => _Email;
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        private string _spnrRelationship;
        public string spnrRelationship
        {
            get => _spnrRelationship;
            set
            {
                if (_spnrRelationship != value)
                {
                    _spnrRelationship = value;
                    OnPropertyChanged(nameof(spnrRelationship));
                }
            }
        }
        private string _PrimaryPhone;
        public string PrimaryPhone
        {
            get => _PrimaryPhone;
            set
            {
                if (_PrimaryPhone != value)
                {
                    _PrimaryPhone = value;
                    OnPropertyChanged(nameof(PrimaryPhone));
                }
            }
        }
        private string _AlternatePhone;
        public string AlternatePhone
        {
            get => _AlternatePhone;
            set
            {
                if (_AlternatePhone != value)
                {
                    _AlternatePhone = value;
                    OnPropertyChanged(nameof(AlternatePhone));
                }
            }
        }
        #endregion

        #region Methods

        public async Task GetUpdateDemographics()
        {
            // Get App settings api..
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    UserDialog.ShowLoading();
                    await Task.Run(async () =>
                    {
                        Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                        {
                            patientProfile = await DataUtility.GetPatientProfileAsync(SettingsValues.ApiURLValue, Token, member is null ? PatientID : (int)member.PatientID).ConfigureAwait(false);
                            //await MaskPhoneNumber();
                            await MaskEmail();
                            txtFirstName = patientProfile.FirstName;
                            txtMiddleName = patientProfile.MiddleName;
                            txtLastName = patientProfile.LastName;
                            txtPreferredName = patientProfile.PreferredName;
                            string dateString = patientProfile.DOB;
                            txtDOB = DateTime.Parse(dateString);
                            spnrGender = patientProfile.Gender;
                            spnrLanguage = patientProfile.Language;
                            spnrRelationship = patientProfile.Relationship;
                            txtOtherRelationship = patientProfile.OtherRelationship;
                            Email = patientProfile.Email;
                            PrimaryPhone = patientProfile.PrimaryPhone;
                            AlternatePhone = patientProfile.AlternatePhone;
                        });

                    }).ConfigureAwait(false);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await App.Current.MainPage.DisplayAlert("", "No Network Connection found, Please Connect to Internet first.", "OK");
                }
                UserDialog.HideLoading();
            }
            catch (Exception ex)
            {
                UserDialog.HideLoading();
            }
        }
        public async Task MaskEmail()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(patientProfile.Email))
                {
                    string pattern = @"(?<=[\w]{0})[\w\-._\+%]*(?=[\w]{1}@)";
                    ResultEmail = Regex.Replace(patientProfile.Email, pattern, m => new string('*', m.Length));
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task MaskPhoneNumber()
        {
            try
            {
                //if (!string.IsNullOrWhiteSpace(patientProfile.PrimaryPhone))
                //{
                //    ResultPhonenumber = "***-***-" + patientProfile.PrimaryPhone.Substring(patientProfile.PrimaryPhone.Length - 4);
                //}
            }
            catch (Exception ex)
            {
            }
        }
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
                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                {
                    //Ask the user if they want to use the camera or pick from the gallery
                    var action = await UserDialogs.Instance.ActionSheetAsync("Add Photo", "Cancel", "", null, "Choose Existing");
                    if ((action == "Choose Existing"))
                    {
                        try
                        {
                            var isPermissionGranted = await Utility.Utilities.RequestCameraAndGalleryPermissions();
                            if (!isPermissionGranted)
                            {
                                await Application.Current.MainPage.DisplayAlert("Warning", "Please allow camera and storage permission to access this functionality.", "OK");
                                return;
                            }
                            MediafileResult = await MediaPicker.PickPhotoAsync();

                           // Helpers.AppGlobalConstants.IsCameraOrGalleryUsed = true;      
                            // canceled
                            if (MediafileResult == null)
                            {

                                return;
                            }
                            // save the file into local storage
                            var newFile = Path.Combine(FileSystem.CacheDirectory, MediafileResult.FileName);

                            MemoryStream mss = new MemoryStream();
                            var stream = await MediafileResult.OpenReadAsync();
                            if (stream != null)
                            {
                                byte[] imageArray = new byte[16 * 1024];

                                using (MemoryStream ms = new MemoryStream())
                                {
                                    int read;
                                    while ((read = stream.Read(imageArray, 0, imageArray.Length)) > 0)
                                    {
                                        ms.Write(imageArray, 0, read);
                                    }
                                    mss = ms;
                                }
                            }

                            var imageSource = ImageSource.FromStream(() => new MemoryStream(mss.ToArray()));
                            if (MediafileResult.FullPath != null)
                            {
                                ProfileImage = MediafileResult.FullPath;
                                Preferences.Set("ProfileImg", ProfileImage);
                            }
                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayAlert("No Media Results", "Alert" + ex.Message, "OK");
                        }
                    }

                    //else if ((action == "Take Photo"))
                    //{
                    //    try
                    //    {
                    //        var isPermissionGranted = await Utility.Utilities.RequestCameraAndGalleryPermissions();
                    //        if (!isPermissionGranted)
                    //        {
                    //            await Application.Current.MainPage.DisplayAlert("Warning", "Please allow camera and storage permission to access this functionality.", "OK");
                    //            return;
                    //        }

                    //        if (!CrossMedia.Current.IsCameraAvailable ||
                    //                   !CrossMedia.Current.IsTakePhotoSupported)
                    //        {
                    //            await Application.Current.MainPage.DisplayAlert("No Camera", "Sorry! No camera available.", "OK");
                    //        }

                    //        MediafileResult = await MediaPicker.CapturePhotoAsync();
                    //      //  Helpers.AppGlobalConstants.IsCameraOrGalleryUsed = true;
                    //        // canceled
                    //        if (MediafileResult == null)
                    //        {
                    //            return;
                    //        }
                    //        //To Save Photo in local storage and get its path 
                    //        var newFilePath = Path.Combine(FileSystem.AppDataDirectory, MediafileResult.FileName);

                    //        MemoryStream mss = new MemoryStream();
                    //        var stream = await MediafileResult.OpenReadAsync();
                    //        byte[] imageArray = new byte[16 * 1024];
                    //        if (stream != null)
                    //        {
                    //            using (MemoryStream ms = new MemoryStream())
                    //            {
                    //                int read;
                    //                while ((read = stream.Read(imageArray, 0, imageArray.Length)) > 0)
                    //                {
                    //                    ms.Write(imageArray, 0, read);
                    //                }
                    //                mss = ms;

                    //                imageArray = mss.ToArray();
                    //            }
                    //        }
                    //        var imageSource = ImageSource.FromStream(() => new MemoryStream(mss.ToArray()));
                    //        if (MediafileResult.FullPath != null)
                    //        {
                    //            ProfileImage = MediafileResult.FullPath;

                    //            if (ProfileImage != null)
                    //                Preferences.Set("ProfileImg", ProfileImage);
                    //        }

                    //        //if (MediafileResult.FullPath != null)
                    //        //{
                    //        //    if (Device.RuntimePlatform == Device.iOS)
                    //        //    {
                    //        //        //To Save Photo in local storage and get its path 
                    //        //        var newFilePath = Path.Combine(FileSystem.AppDataDirectory, MediafileResult.FileName);

                    //        //        var img = ImageService.Instance.LoadStream(async c => await MediafileResult.OpenReadAsync());


                    //        //       // var img = await MediafileResult.OpenReadAsync();

                    //        //        // iOS doesn't rotate the image to match the device orientation, so we have to rotate it 90 degrees here
                    //        //        if (Device.RuntimePlatform == Device.iOS)
                    //        //        {
                    //        //            if (img.Transformations != null)
                    //        //            {
                    //        //                var angle = img.Transformations;
                    //        //            }
                    //        //            img.Transform(new RotateTransformation(90));
                    //        //        }

                    //        //        // Get the transformed image as a PNG
                    //        //        using var imageStream = await img.AsPNGStreamAsync();

                    //        //        //using (var streamm = await MediafileResult.OpenReadAsync())
                    //        //        using (var streamm = imageStream)
                    //        //        {
                    //        //            File.Create(newFilePath).Dispose();
                    //        //            using (var writer = new StreamWriter(newFilePath))
                    //        //            {
                    //        //                await streamm.CopyToAsync(writer.BaseStream);
                    //        //                if (newFilePath != null)
                    //        //                {
                    //        //                    ProfileImage = newFilePath;
                    //        //                }
                    //        //                await UpdateProfilePicture();
                    //        //            }
                    //        //        }
                    //        //    }

                    //        //    else
                    //        //    {
                    //        //        ProfileImage = MediafileResult.FullPath;

                    //        //        if (ProfileImage != null)
                    //        //            await UpdateProfilePicture();
                    //        //    }
                    //        //}
                    //    }
                    //    //else
                    //    //{
                    //    //    var path = DependencyService.Get<IMediaService>().SaveImageFromByte(mss.ToArray(), MediafileResult.FileName, null);
                    //    //    if (path != null)
                    //    //    {
                    //    //        ProfileImage = path;

                    //    //        if (ProfileImage != null)
                    //    //            UpdateProfilePicture();
                    //    //    }
                    //    //}
                    //    catch (Exception ex)
                    //    {
                    //        await Application.Current.MainPage.DisplayAlert("No Media Results", "Alert" + ex.Message, "OK");
                    //    }
                    //}
                });
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
