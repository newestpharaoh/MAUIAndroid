using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.Media;
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
                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                {
                    //Ask the user if they want to use the camera or pick from the gallery
                    var action = await UserDialogs.Instance.ActionSheetAsync("Add Photo", "Cancel", "", null, "Choose Existing", "Take Photo");
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
