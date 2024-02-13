using System.Collections.Generic;
using System.Linq;

namespace CommonLibraryCoreMaui.Models
{
    public class AdditionalFamilyMember
    {
        public PatientProfile FamilyMemberInformation { get; set; }
        public MedicalInfo MedicalHistory { get; set; }
    }

    public class AccountAddFamilyMember
    {
        private static AccountAddFamilyMember m_oInstance = null;
        private static readonly object m_oPadLock = new object();

        public static AccountAddFamilyMember Instance
        {
            get
            {
                lock (m_oPadLock)
                {
                    if (m_oInstance == null)
                    {
                        m_oInstance = new AccountAddFamilyMember();
                    }
                    return m_oInstance;
                }
            }
        }

        public int PrimaryPatientID { get; set; }
        public string AdditionalCost { get; set; }
        public string SingleAdditionalCost { get; set; }
        public string ProratedCost { get; set; }
        public int FreeFamilyMemberRemaining { get; set; }
        public bool IncludedInPlan { get; set; }
        public int InitialFreeFamilyMemberRemaining { get; set; }
        public List<AdditionalFamilyMember> AdditionalFamilyMembers { get; set; }
        public List<AccountMember> ExistingFamilyMembers { get; set; }
        public List<AddedFamilyMembers> AddedFamilyMembersNames { get; set; }

        public void Clear()
        {
            AdditionalFamilyMembers = null;
            PrimaryPatientID = 0;
            FreeFamilyMemberRemaining = -1;
            IncludedInPlan = false;
            InitialFreeFamilyMemberRemaining = -1;
            AdditionalCost = string.Empty;
            SingleAdditionalCost = string.Empty;
            ProratedCost = string.Empty;
        }

		public void ClearNameList()
		{
            AddedFamilyMembersNames = null;
        }

        public void AddAdditionalFamilyMember(AdditionalFamilyMember member)
        {
            if (AdditionalFamilyMembers is null) AdditionalFamilyMembers = new List<AdditionalFamilyMember>();
            AdditionalFamilyMembers.Add(member);
        }

        public void UpdateFamilyMembersNameList(AdditionalFamilyMember member, bool isFreeMember)
        {
            if (AddedFamilyMembersNames is null) AddedFamilyMembersNames = new List<AddedFamilyMembers>();
            AddedFamilyMembersNames.Add(new AddedFamilyMembers()
            {
                FullName = member.FamilyMemberInformation.FullName,
				IsFreeMember = isFreeMember
            });
        }

        public bool IsDuplicate(string firstName, string lastName, string dob)
        {
            bool ret = false;

            try
            {
                if (ExistingFamilyMembers != null)
                {
                    ret = ExistingFamilyMembers.Any(x => x.FirstName == firstName && x.LastName == lastName && x.DOB == dob);
                }
            }
            catch { }

            return ret;
        }
    }

	public class AddedFamilyMembers
	{
        public string FullName { get; set; }
		public bool IsFreeMember { get; set; }
	}

    public class AccountAddFamilyMemberState
    {
        public int PrimaryPatientID { get; set; }
        public string AdditionalCost { get; set; }
        public int FreeFamilyMemberRemaining { get; set; }
        public List<AdditionalFamilyMember> AdditionalFamilyMembers { get; set; }
        public List<AccountMember> ExistingFamilyMembers { get; set; }
        public string ProratedAddOnCost { get; set; }
        public string SingleAddOnCost { get; set; }
        public bool NoActiveCardOnFile { get; set; }
        public bool AddingFamilyMembersProcessCancelled { get; set; }

        public bool IsDuplicate(string firstName, string lastName, string dob)
        {
            bool ret = false;

            try
            {
                if (ExistingFamilyMembers is null) ExistingFamilyMembers = new List<AccountMember>();

                if (AdditionalFamilyMembers != null)
                {
                    foreach (AdditionalFamilyMember afm in AdditionalFamilyMembers)
                    {
                        ExistingFamilyMembers.Add(new AccountMember() { FirstName = afm.FamilyMemberInformation.FirstName, LastName = afm.FamilyMemberInformation.LastName, DOB = afm.FamilyMemberInformation.DOB });
                    }
                }

                ret = ExistingFamilyMembers.Any(x => x.FirstName == firstName && x.LastName == lastName && x.DOB == dob);
            }
            catch { }

            return ret;
        }
    }

    public class FamilyMemberOperations
	{
        public string UpdateDemographics { get; set; } = "Update Demographics";
        public string UpdateMedicalInfo { get; set; } = "Update Medical Information";
        public string UpdateAccountAccess { get; set; } = "Update Account Access";
    }
}
