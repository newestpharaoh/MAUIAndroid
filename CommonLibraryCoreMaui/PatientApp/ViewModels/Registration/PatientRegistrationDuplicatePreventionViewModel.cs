using CommonLibraryCoreMaui.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.PatientApp.ViewModels
{
    public class PatientRegistrationDuplicatePreventionViewModel : MvxViewModel
    {
        private string ApiUrl;

        private string firstName;
        public string FirstName
        {
            get { return !string.IsNullOrEmpty(Registration.Instance.FirstName) ? Registration.Instance.FirstName : firstName; }
            set { SetProperty(ref firstName, value); }
        }

        private string lastName;
        public string LastName
        {
            get { return !string.IsNullOrEmpty(Registration.Instance.LastName) ? Registration.Instance.LastName : lastName; }
            set { SetProperty(ref lastName, value); }
        }

        private string dob;
        public string DOB
        {
            get
            {
                if (!string.IsNullOrEmpty(Registration.Instance.DOB))
                {
                    DateTime dtDOB = DateTime.Parse(Registration.Instance.DOB);
                    return dtDOB.ToString("MM/dd/yyyy");
                }
                else
                {
                    if (string.IsNullOrEmpty(dob))
                    {
                        return new DateTime(1980, 1, 1).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        return dob;
                    }
                }
            }
            set { SetProperty(ref dob, value); }
        }

        public PatientRegistrationDuplicatePreventionViewModel(string apiUrl)
        {
            ApiUrl = apiUrl;
        }

        public IMvxCommand SubmitCommand => new MvxAsyncCommand(Submit);

        public async Task Submit()
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(dob))
            {
                //UtilsUI.ShowMsgOkScreen(this, "All fields are required!");
            }
            else
            {
                StatusResponse resp = await DataUtility.FindPatientAsync(ApiUrl, firstName, lastName, dob).ConfigureAwait(false);

                if (resp != null)
                {
                    //Intent intent = null;

                    switch (resp.StatusCode)
                    {
                        case StatusCode.Payload:
                            if (!string.IsNullOrEmpty(resp.Payload))
                            {
                                //UtilsUI.ShowMsgOkScreen(this, resp.Payload);
                            }
                            break;
                        case StatusCode.NotFound:
                        case StatusCode.InActiveMatchFound:
                            //intent = new Intent(this, typeof(PatientRegistrationPaymentPlanScreenActivity));
                            Registration.Instance.FirstName = firstName;
                            Registration.Instance.LastName = lastName;
                            Registration.Instance.DOB = dob;
                            Registration.Instance.IsSelfPay = true;
                            Registration.Instance.MPI = string.IsNullOrEmpty(resp.Payload) ? null : resp.Payload;
                            break;
                        case StatusCode.EmailAlreadyInUse:
                        case StatusCode.AlreadyRegistered:
                            //mvvm navigation here!
                            //intent = new Intent(this, typeof(PatientRegistrationSingleMatchFoundActivity));
                            break;
                        case StatusCode.ActivationEmailSent:
                            //intent = new Intent(this, typeof(PatientRegistrationActivationEmailSentActivity));
                            //intent.PutExtra("emailAddress", resp.Payload);
                            break;
                        case StatusCode.NotPolicyHolder:
                            //intent = new Intent(this, typeof(PatientRegistrationNotPolicyHolderActivity));
                            break;
                        case StatusCode.MultipleMatches:
                            //intent = new Intent(this, typeof(PatientRegistrationMultipleRecordsFoundActivity));
                            break;
                    }
                }
            }
        }
    }
}
