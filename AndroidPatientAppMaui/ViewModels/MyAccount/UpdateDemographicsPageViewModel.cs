using Acr.UserDialogs;
using AndroidPatientAppMaui.Models;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using System.Text.RegularExpressions;

namespace AndroidPatientAppMaui.ViewModels.MyAccount
{
    public class UpdateDemographicsPageViewModel : BaseViewModel
    {
        //To define the class level variable.
        public BasicFamilyMemberInfo member;
        public PatientProfile patientProfile;
        UserInfo userInfo;
        GlobalState global;
        int PatientID = 0;
        int UserId = 0;
        string Token = string.Empty;
        public string[] titleItems;
        #region Constructor
        public UpdateDemographicsPageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);
                ChangeProfilePhotoCommand = new Command(ChangeProfilePhotoAsync);
                ChangePasswordCommand = new Command(ChangePasswordAsync);
                SaveChangesCommand = new Command(SaveChangesAsync);

                Token = Preferences.Get("AuthToken", string.Empty);
                UserId = Preferences.Get("UserId", 0);
                PatientID = Preferences.Get("PatientID", 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
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


        private string _ProfileImage = "usercircle.png";
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
        private bool _AlternatePhoneIsVisible = true;
        public bool AlternatePhoneIsVisible
        {
            get { return _AlternatePhoneIsVisible; }
            set
            {
                if (_AlternatePhoneIsVisible != value)
                {
                    _AlternatePhoneIsVisible = value;
                    OnPropertyChanged("AlternatePhoneIsVisible");
                }
            }
        }
        private bool _Address1IsVisible = true;
        public bool Address1IsVisible
        {
            get { return _Address1IsVisible; }
            set
            {
                if (_Address1IsVisible != value)
                {
                    _Address1IsVisible = value;
                    OnPropertyChanged("Address1IsVisible");
                }
            }
        }
        private bool _Address2IsVisible = true;
        public bool Address2IsVisible
        {
            get { return _Address2IsVisible; }
            set
            {
                if (_Address2IsVisible != value)
                {
                    _Address2IsVisible = value;
                    OnPropertyChanged("Address2IsVisible");
                }
            }
        }
        private bool _CityIsVisible = true;
        public bool CityIsVisible
        {
            get { return _CityIsVisible; }
            set
            {
                if (_CityIsVisible != value)
                {
                    _CityIsVisible = value;
                    OnPropertyChanged("CityIsVisible");
                }
            }
        }
        private bool _ZipcodeIsVisble = true;
        public bool ZipcodeIsVisble
        {
            get { return _ZipcodeIsVisble; }
            set
            {
                if (_ZipcodeIsVisble != value)
                {
                    _ZipcodeIsVisble = value;
                    OnPropertyChanged("ZipcodeIsVisble");
                }
            }
        }
        private bool _spnrStateIsVisble = true;
        public bool spnrStateIsVisble
        {
            get { return _spnrStateIsVisble; }
            set
            {
                if (_spnrStateIsVisble != value)
                {
                    _spnrStateIsVisble = value;
                    OnPropertyChanged("spnrStateIsVisble");
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
        private string _spnrState;
        public string spnrState
        {
            get { return _spnrState; }
            set
            {
                if (_spnrState != value)
                {
                    _spnrState = value;
                    OnPropertyChanged("spnrState");
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
        private string _Title;
        public string Title
        {
            get => _Title;
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    OnPropertyChanged(nameof(Title));
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
                    OnPropertyChanged("spnrLanguage");
                }
            }
        }
        private string _txtEmail;
        public string txtEmail
        {
            get => _txtEmail;
            set
            {
                if (_txtEmail != value)
                {
                    _txtEmail = value;
                    OnPropertyChanged("txtEmail");
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
                    OnPropertyChanged("spnrRelationship");
                }
            }
        }
        private int _spnrTitleSelectedIndex;
        public int spnrTitleSelectedIndex
        {
            get => _spnrTitleSelectedIndex;
            set
            {
                if (_spnrTitleSelectedIndex != value)
                {
                    _spnrTitleSelectedIndex = value;
                    OnPropertyChanged("spnrTitleSelectedIndex");
                }
            }
        }
        private int _spnrRelationshipselectedindex;
        public int spnrRelationshipselectedindex
        {
            get => _spnrRelationshipselectedindex;
            set
            {
                if (_spnrRelationshipselectedindex != value)
                {
                    _spnrRelationshipselectedindex = value;
                    OnPropertyChanged("spnrRelationshipselectedindex");
                }
            }
        }
        private string _txtAlternatePhone;
        public string txtAlternatePhone
        {
            get => _txtAlternatePhone;
            set
            {
                if (_txtAlternatePhone != value)
                {
                    _txtAlternatePhone = value;
                    OnPropertyChanged("txtAlternatePhone");
                }
            }
        }
        private List<string> _spnrTitle = CommonLibraryCoreMaui.Theme.Values.UserTitles;
        public List<string> spnrTitle
        {
            get { return _spnrTitle; }
            set
            {
                if (_spnrTitle != value)
                {
                    _spnrTitle = value;
                    OnPropertyChanged("spnrTitle");
                }
            }
        }
        private List<string> _GenderList = CommonLibraryCoreMaui.Theme.Values.GenderOptions;
        public List<string> GenderList
        {
            get { return _GenderList; }
            set
            {
                if (_GenderList != value)
                {
                    _GenderList = value;
                    OnPropertyChanged("GenderList");
                }
            }
        }
        private string[] _languages = { "English", "Spanish" };
        public string[] Languages
        {
            get => _languages;
            set
            {
                _languages = value;
                OnPropertyChanged();
            }
        }
        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged();
            }
        }
        private string _selectedRelationship;
        public string SelectedRelationship
        {
            get => _selectedRelationship;
            set
            {
                _selectedRelationship = value;
                OnPropertyChanged();
            }
        }
        private string _SelectedState;
        public string SelectedState
        {
            get => _SelectedState;
            set
            {
                _SelectedState = value;
                OnPropertyChanged();
            }
        }
        private string _txtAddress1;
        public string txtAddress1
        {
            get => _txtAddress1;
            set
            {
                _txtAddress1 = value;
                OnPropertyChanged("txtAddress1");
            }
        }
        private string _txtAddress2;
        public string txtAddress2
        {
            get => _txtAddress2;
            set
            {
                _txtAddress2 = value;
                OnPropertyChanged("txtAddress2");
            }
        }
        private string _txtPrimaryPhone;
        public string txtPrimaryPhone
        {
            get => _txtPrimaryPhone;
            set
            {
                _txtPrimaryPhone = value;
                OnPropertyChanged("txtPrimaryPhone");
            }
        }
        private string _txtCity;
        public string txtCity
        {
            get => _txtCity;
            set
            {
                _txtCity = value;
                OnPropertyChanged("txtCity");
            }
        }
        private string _txtZipcode;
        public string txtZipcode
        {
            get => _txtZipcode;
            set
            {
                _txtZipcode = value;
                OnPropertyChanged("txtZipcode");
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// TODO : To Define the Get Update Demographic Method....
        /// </summary>
        /// <returns></returns>
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
                            try
                            {
                                patientProfile = await DataUtility.GetPatientProfileAsync(SettingsValues.ApiURLValue, Token, member is null ? PatientID : (int)member.PatientID).ConfigureAwait(false);
                                if (patientProfile != null)
                                {
                                    //if (string.IsNullOrEmpty(patientProfile.Photo))
                                    //{
                                    //    Android.Graphics.Drawables.BitmapDrawable bd = (Android.Graphics.Drawables.BitmapDrawable)sPhoto.Drawable;
                                    //    //imageBitmap = CropImage.ToOvalBitmap(bd.Bitmap); //TODO
                                    //}
                                    //else
                                    //{
                                    //    Bitmap bm = await ProfileImageHelper.GetImageBitmapFromUrl(patientProfile.Photo).ConfigureAwait(false);
                                    //    //imageBitmap = PatientApp.Utils.Resize(CropImage.ToOvalBitmap(bm), 500, 500); //TODO
                                    //}
                                    Title = patientProfile.Title;
                                    await MaskPhoneNumber();
                                    await MaskEmail();
                                    ProfileImage = patientProfile.Photo;
                                    txtFirstName = patientProfile.FirstName;
                                    txtMiddleName = patientProfile.MiddleName;
                                    txtLastName = patientProfile.LastName;
                                    txtPreferredName = patientProfile.PreferredName;
                                    string dateString = patientProfile.DOB;
                                    txtDOB = DateTime.Parse(dateString);
                                    spnrGender = patientProfile.Gender;
                                    spnrState = patientProfile.State;
                                    spnrLanguage = patientProfile.Language;
                                    spnrRelationship = patientProfile.Relationship;
                                    txtOtherRelationship = patientProfile.OtherRelationship;
                                    txtEmail = patientProfile.Email;
                                    txtPrimaryPhone = patientProfile.PrimaryPhone;
                                    txtAlternatePhone = patientProfile.AlternatePhone;

                                    if (!string.IsNullOrEmpty(patientProfile.AlternatePhone)) txtAlternatePhone = patientProfile.AlternatePhone;
                                    txtAddress1 = patientProfile.Address1;
                                    txtAddress2 = patientProfile.Address2;
                                    txtCity = patientProfile.City;
                                    SelectedState = patientProfile.State;
                                    if (!string.IsNullOrEmpty(patientProfile.Zip))
                                    {
                                        txtZipcode = patientProfile.Zip;
                                    }

                                    if (string.IsNullOrEmpty(patientProfile.NotificationPreference))
                                    {
                                        rbtnEmailNotification = true;
                                    }
                                    else
                                    {
                                        if (patientProfile.NotificationPreference.ToLower() == "email")
                                        {
                                            rbtnEmailNotification = true;
                                        }
                                        else
                                        {
                                            rbtnTextNotification = true;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(patientProfile.Title))
                                    {
                                        int selectedIndex = Array.IndexOf(titleItems, patientProfile.Title) + 1;
                                        if (selectedIndex >= 0 && selectedIndex < spnrTitle.Count)
                                        {
                                            spnrTitleSelectedIndex = selectedIndex;
                                        }
                                    }
                                    // Set the selected language based on the condition
                                    SelectedLanguage = patientProfile.LanguageID == 1 ? "English" : "Spanish";

                                    if (!string.IsNullOrEmpty(patientProfile.Relationship))
                                    {
                                        if (!string.IsNullOrEmpty(patientProfile.OtherRelationship))
                                        {
                                            spnrRelationshipselectedindex = RelationshipList.Count;
                                            txtOtherRelationship = patientProfile.OtherRelationship;
                                            lytOtherRelationship = true;
                                        }
                                    }
                                    else
                                    {
                                        lytOtherRelationship = false;


                                    }

                                }
                            }
                            catch (Exception ex)
                            {

                            }
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
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// TODO : To define the Mask Email....
        /// </summary>
        /// <returns></returns>
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
                Console.WriteLine(ex);
            }
        }
         
        /// <summary>
        /// TODO : To Define the Mask Phone Number;
        /// </summary>
        /// <returns></returns>
        public async Task MaskPhoneNumber()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(patientProfile.PrimaryPhone) && patientProfile.PrimaryPhone.Length >= 10)
                {
                    string areaCode = patientProfile.PrimaryPhone.Substring(0, 3);
                    string prefix = patientProfile.PrimaryPhone.Substring(3, 3);
                    string lastDigits = patientProfile.PrimaryPhone.Substring(10); // Take all digits from the 6th position

                    string maskedAreaCode = "***";
                    string maskedPrefix = "***";

                    ResultPhonenumber = $"({maskedAreaCode}) {maskedPrefix}-{lastDigits}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
                                await UpdateProfilePicture();
                            }
                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayAlert("No Media Results", "Alert" + ex.Message, "OK");
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        /// <summary>
        /// To update profile picture
        /// </summary>
        /// <returns></returns>
        public async Task UpdateProfilePicture()
        {
            //Call api..
            try
            {
                UserDialogs.Instance.ShowLoading("Uploading Image...", MaskType.Clear);
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    await Task.Run(async () =>
                    {
                        if (_businessCode != null)
                        {
                            UpdateProfileImgReqModel updateprofile = new UpdateProfileImgReqModel()
                            {
                                imgPath = ProfileImage,
                                uri = $"{SettingsValues.ApiURLValue}/Patient/UpdatePhoto?patientID={patientProfile.PatientID}",
                                token = Preferences.Get("AuthToken", "")
                            };
                            await _businessCode.UpdateProfileImgApi(updateprofile, MediafileResult,
                            async (objRes) =>
                            {
                                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                {
                                    var res = objRes as StatusResponse;
                                    Preferences.Set("ProfileImg", res.Payload);
                                    await UserDialogs.Instance.AlertAsync("Patient Profile Photo Updated Successfully.", "Success", "Ok");
                                    UserDialogs.Instance.HideLoading();
                                });
                            }, (objj) =>
                            {
                                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                {
                                    UserDialogs.Instance.HideLoading();
                                    await UserDialogs.Instance.AlertAsync("Something went wrong!  Please try again.");
                                });
                            });
                        }
                    }).ConfigureAwait(false);

                    UserDialogs.Instance.HideLoading();
                }
                else
                {
                    UserDialogs.Instance.Loading().Hide();
                }
            }
            catch (Exception ex)
            { 
                UserDialog.HideLoading();
                await Task.CompletedTask;
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
                if (patientProfile != null)
                {
                    bool bRelationship = false;
                    if (!string.IsNullOrEmpty(patientProfile.Relationship))
                    {
                        if (patientProfile.Relationship.IndexOf("box below", StringComparison.InvariantCultureIgnoreCase) > -1)
                        {
                            bRelationship = string.IsNullOrEmpty(txtOtherRelationship.Trim());
                        }
                    }

                    bool notvalid = true;
                    if (member != null)
                    {
                        notvalid = string.IsNullOrEmpty(txtFirstName.Trim()) ||
                           string.IsNullOrEmpty(txtLastName.Trim()) ||
                           string.IsNullOrEmpty(txtPrimaryPhone.Trim()) ||
                            string.IsNullOrEmpty(txtDOB.ToString().Trim()) ||
                            bRelationship ||
                             string.IsNullOrEmpty(txtEmail.Trim()) ||
                             string.IsNullOrEmpty(patientProfile.Gender);
                    }
                    else
                    {
                        notvalid = string.IsNullOrEmpty(txtFirstName.Trim()) ||
                           string.IsNullOrEmpty(txtLastName.Trim()) ||
                           string.IsNullOrEmpty(txtPrimaryPhone.Trim()) ||
                           string.IsNullOrEmpty(txtAddress1.Trim()) ||
                           string.IsNullOrEmpty(txtZipcode.Trim()) ||
                           string.IsNullOrEmpty(txtCity.Trim()) ||
                           string.IsNullOrEmpty(txtAlternatePhone.Trim()) ||
                           string.IsNullOrEmpty(txtDOB.ToString().Trim()) ||
                           string.IsNullOrEmpty(txtEmail.Trim()) ||
                            bRelationship ||
                            string.IsNullOrEmpty(patientProfile.Gender);

                    }

                    if (notvalid)
                    {
                        UserDialog.Alert("Please fill all the required fields!");
                    }
                    else
                    {
                        patientProfile.FirstName = txtFirstName;
                        patientProfile.MiddleName = txtMiddleName;
                        patientProfile.LastName = txtLastName;
                        patientProfile.Email = txtEmail;
                        patientProfile.PrimaryPhone = txtPrimaryPhone;
                        patientProfile.PreferredName = txtPreferredName;
                        patientProfile.AlternatePhone = txtAlternatePhone;
                        patientProfile.Address1 = txtAddress1;
                        patientProfile.Address2 = txtAddress2;
                        patientProfile.City = txtCity;
                        patientProfile.Zip = txtZipcode;
                        patientProfile.NotificationPreference = rbtnEmailNotification ? "email" : "text";
                        patientProfile.DOB = txtDOB.ToString();
                        if (member != null)
                        {
                            patientProfile.OtherRelationship = "no-update";
                            patientProfile.Relationship = "no-update";
                        }
                        else
                        {
                            patientProfile.OtherRelationship = null;
                            patientProfile.Relationship = null;
                        }
                        StatusResponse resp = await DataUtility.UpdatePatientProfileAsync(SettingsValues.ApiURLValue, Token, patientProfile).ConfigureAwait(false);
                        if (resp != null)
                        {
                            if (resp.StatusCode == StatusCode.Success)
                            {
                                if (member is null)
                                {

                                    try
                                    {
                                        if (Helpers.AppGlobalConstants.userInfo != null)
                                        {
                                            UserInfo userInfo = await DataUtility.GetUserInfo(SettingsValues.ApiURLValue, UserId, true, Token).ConfigureAwait(false);
                                            Helpers.AppGlobalConstants.userInfo = userInfo;
                                            Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                            {
                                                await Navigation.PopModalAsync();
                                            });
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                                else
                                {
                                    Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                                    {
                                        await Navigation.PopModalAsync();
                                    });
                                }

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion
    }
}
