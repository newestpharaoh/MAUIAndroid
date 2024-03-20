using Acr.UserDialogs;
using CommonLibraryCoreMaui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.Family
{
    public class PatientSettingsManageSubscriptionAddFamilyMemberViewModel : BaseViewModel
    {
        //To define the class level variable.
        int PatientID = 0;
        int UserId = 0;
        string Token = string.Empty;
        public PatientProfile patientProfile;
        #region Constructor
        public PatientSettingsManageSubscriptionAddFamilyMemberViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                BackCommand = new Command(BackAsync);
                CancelCommand = new Command(CancelCommandAsync);
                ContinueCommand = new Command(ContinueAsync);

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
        public Command CancelCommand { get; set; }
        public Command ContinueCommand { get; set; }
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

        private bool _lytRelationship = true;
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

        private string _txtOtherRelationship = string.Empty;
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
        private string _txtFirstName = string.Empty;
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
        private string _txtMiddleName = string.Empty;
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
        private string _txtLastName = string.Empty;
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
        private string _txtPhoneNumber = string.Empty;
        public string txtPhoneNumber
        {
            get { return _txtPhoneNumber; }
            set
            {
                if (_txtPhoneNumber != value)
                {
                    _txtPhoneNumber = value;
                    OnPropertyChanged("txtPhoneNumber");
                }
            }
        }
        private DateTime _txtDOB = new DateTime();
        public DateTime txtDOB
        {
            get => _txtDOB;
            set
            {
                if (_txtDOB != value)
                {
                    _txtDOB = value;
                    OnPropertyChanged("txtDOB");
                }
            }
        }
        private string _Title = string.Empty;
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
        private string _spnrGender = string.Empty;
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
        private string _txtEmail = string.Empty;
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
        private string _spnrRelationship = string.Empty;
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
        private int _GenderSelectedIndex;
        public int GenderSelectedIndex
        {
            get => _GenderSelectedIndex;
            set
            {
                if (_GenderSelectedIndex != value)
                {
                    _GenderSelectedIndex = value;
                    OnPropertyChanged("GenderSelectedIndex");
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
        private string _txtAlternatePhone = string.Empty;
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
        private string _selectedRelationship = string.Empty;
        public string SelectedRelationship
        {
            get => _selectedRelationship;
            set
            {
                _selectedRelationship = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods
      
        private async void ContinueAsync(object obj)
        {
            try
            {
                //bool bRelationship = false;
                //patientProfile = new PatientProfile();
                //if (!string.IsNullOrEmpty(spnrRelationship))
                //{
                //    if (patientProfile.Relationship.IndexOf("box below", StringComparison.InvariantCultureIgnoreCase) > -1)
                //    {
                //        bRelationship = string.IsNullOrEmpty(txtOtherRelationship.Trim());
                //    }
                //}
                //else
                //{
                //    bRelationship = true;
                //}

                if (string.IsNullOrEmpty(txtFirstName.Trim()) ||
                 string.IsNullOrEmpty(txtLastName.Trim()) ||
                string.IsNullOrEmpty(txtPhoneNumber.Trim()) ||
                   string.IsNullOrEmpty(txtDOB.ToString().Trim()) ||
                 string.IsNullOrEmpty(spnrGender) ||
                    string.IsNullOrEmpty(spnrRelationship) 
                 )
                {
                    UserDialog.Alert("Please fill all the required fields!");
                }
                else
                {
                    bool isDuplicate;
                    //using (AccountAddFamilyMemberStateHelper afmStateHelper = new AccountAddFamilyMemberStateHelper(this))
                    //{
                    //    AccountAddFamilyMemberState state = afmStateHelper.GetState();
                    //    isDuplicate = state.IsDuplicate(txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtDOB.Text.Trim());

                    //}
                    AccountAddFamilyMemberState state = Helpers.AppGlobalConstants.state;
                    isDuplicate = state.IsDuplicate(txtFirstName.Trim(), txtLastName.Trim(), txtDOB.ToString().Trim());
                    if (isDuplicate)
                    {
                        UserDialog.Alert("Family member already exist!");
                    }
                    else
                    {
                        patientProfile = new PatientProfile();
                        patientProfile.FirstName = txtFirstName.Trim() != null ? txtFirstName.Trim() : "";
                        patientProfile.LastName = txtLastName.Trim() != null ? txtLastName.Trim() : "";
                        patientProfile.MiddleName = txtMiddleName.Trim() != null ? txtMiddleName.Trim() : "";
                        patientProfile.DOB = txtDOB.ToString().Trim() != null ? txtDOB.ToString().Trim() : "";
                        patientProfile.OtherRelationship = txtOtherRelationship.Trim() != null ? txtOtherRelationship.Trim() : "";
                        patientProfile.PrimaryPhone = txtPhoneNumber != null ? txtPhoneNumber.Trim() : "";
                        patientProfile.Title = Title != null ? Title.Trim() : "";
                        patientProfile.Gender = spnrGender != null ? spnrGender.Trim() : "";
                        patientProfile.Relationship = spnrRelationship != null ? spnrRelationship.Trim() : "";


                        AdditionalFamilyMember additionalFamilyMember = new AdditionalFamilyMember();
                        additionalFamilyMember.FamilyMemberInformation = patientProfile;
                        var title = $"{patientProfile.FirstName} {patientProfile.LastName}";
                        await Navigation.PushModalAsync(new Views.MyMedicalInfo.MyMedicalInfoDetailsPage(title, null, additionalFamilyMember, null, null), false);
                        //var intent = new Intent(this, typeof(PatientRegistrationMedicalInfoActivity));
                        //AdditionalFamilyMember additionalFamilyMember = new AdditionalFamilyMember();
                        //additionalFamilyMember.FamilyMemberInformation = member;
                        //intent.PutExtra("additionalFamilyMember", additionalFamilyMember);
                        //intent.PutExtra("title", $"{member.FirstName} {member.LastName}");
                        //StartActivity(intent);
                        //Finish();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private async void CancelCommandAsync(object obj)
        {
            try
            {
                await Navigation.PopModalAsync();
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

        #endregion
    }
}
