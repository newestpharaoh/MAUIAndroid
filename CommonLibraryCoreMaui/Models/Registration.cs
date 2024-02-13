using System;
using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public sealed class Registration
    {
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string PatientID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MPI { get; set; }
        public string Domain { get; set; }
        public bool IsPrimary { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateAdded { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string CardFirstName { get; set; }
        public string CardLastName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationMonth { get; set; }
        public string CardExpirationYear { get; set; }
        public string CardSecurityCode { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string PromoCode { get; set; }
        public string ResignupCode { get; set; }
        public int SubscriptionOptionID { get; set; }
        public List<SecurityQuestionAnswer> Answers { get; set; }
        public string PreferredName { get; set; }
        public string Language { get; set; }
        public bool? Established { get; set; }

        public bool IsSelfPay { get; set; }

        public Subscription Subscription { get; set; }

        public bool IsInProgress { get; set; } = true;

        private static Registration m_oInstance = null;
        private static readonly object m_oPadLock = new object();

        public static Registration Instance
        {
            get
            {
                lock (m_oPadLock)
                {
                    if (m_oInstance == null)
                    {
                        m_oInstance = new Registration();
                    }
                    return m_oInstance;
                }
            }
        }

        public void Clear()
        {
            this.Subscription = null;
            this.Answers = null;
            PatientID = "0";
            Email = string.Empty;
            Password = string.Empty;
            MPI = string.Empty;
            FirstName = string.Empty;
            MiddleName = string.Empty;
            LastName = string.Empty;
            DOB = string.Empty;
            Gender = string.Empty;
            Phone = string.Empty;
            Street1 = string.Empty;
            Street2 = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Zip = string.Empty;
            CardFirstName = string.Empty;
            CardLastName = string.Empty;
            CardNumber = string.Empty;
            CardExpirationMonth = string.Empty;
            CardExpirationYear = string.Empty;
            CardSecurityCode = string.Empty;
            BillingAddress = string.Empty;
            BillingCity = string.Empty;
            BillingState = string.Empty;
            BillingZip = string.Empty;
            PromoCode = string.Empty;
            ResignupCode = string.Empty;
            SubscriptionOptionID = 0;
            PreferredName = string.Empty;
            Message = string.Empty;
        }

        public void Init(Registration registration)
        {
            PatientID = registration.PatientID;
            Email = registration.Email;
            Password = registration.Password;
            MPI = registration.MPI;
            FirstName = registration.FirstName;
            MiddleName = registration.MiddleName;
            LastName = registration.LastName;
            DOB = registration.DOB;
            Gender = registration.Gender;
            Phone = registration.Phone;
            Street1 = registration.Street1;
            Street2 = registration.Street2;
            City = registration.City;
            State = registration.State;
            Zip = registration.Zip;
            Domain = registration.Domain;
        }

        public void Init(RegistrationUserInfo registration)
        {
            PatientID = registration.PatientID.ToString();
            Email = registration.Email;
            Password = registration.Password;
            MPI = Convert.ToString(registration.MPI);
            FirstName = registration.FirstName;
            MiddleName = registration.MiddleName;
            LastName = registration.LastName;
            DOB = registration.DOB;
            Gender = registration.Gender;
            Phone = registration.Phone;
            Street1 = registration.Street1;
            Street2 = registration.Street2;
            City = registration.City;
            State = registration.State;
            Zip = registration.Zip;
            Domain = registration.Domain;
        }
    }
}