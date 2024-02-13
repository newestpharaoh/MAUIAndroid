using System.Collections.Generic;
using CommonLibraryCoreMaui.Models;
using MvvmCross.Commands;

namespace CommonLibraryCoreMaui
{
    public static class FamilyOptionsFactory
	{
		static Dictionary<MemberType, AccountMemberOptions> MemberOptionsCombinations;
		static FamilyOptionsFactory()
		{
			MemberOptionsCombinations = new Dictionary<MemberType, AccountMemberOptions>(4)
			{
				{
					MemberType.Private,
					new AccountMemberOptions()
					{
						MemberType = MemberType.Private,
						Title = "PRIVATIZE User Account",
						Description = "Create a separate sign in to fully privatize a member's medical information. The member will remain on this family account.",
						Image = "privatize_user",
						ActionTitle = "Privatize User"
					}
				},
				{
					MemberType.Reactivate,
					new AccountMemberOptions()
					{
						MemberType = MemberType.Reactivate,
						Title = "REACTIVATE User Account",
						Description = "Member will be reactivated and able to visit with a provider immediately.",
						Image = "reactivate_user",
						ActionTitle = "Reactivate User"
					}
				},
				{
					MemberType.Deactivate,
					new AccountMemberOptions()
					{
						MemberType = MemberType.Deactivate,
						Title = "DEACTIVATE User Account",
						Description = "Member will be able to access previous records, but may not initiate a new visit. Member becomes inactive at end of billing period.",
						Image = "deactivate_user",
						ActionTitle = "Deactivate"
					}
				},
				{
					MemberType.Remove,
					new AccountMemberOptions()
					{
						MemberType = MemberType.Remove,
						Title = "REMOVE User from Family Account",
						Description = "Remove the member from this account. The member will no longer have any access through this family account but will be able to create a separate account.",
						Image = "remove_user",
						ActionTitle = "Remove User"
					}
				},

			};
		}

		public static List<AccountMemberOptions> Get(AccountMember accountMember)
		{
			var lstAccountMemberOptions = new List<AccountMemberOptions>();
			lstAccountMemberOptions.Add(MemberOptionsCombinations[MemberType.Private]);

			if (!accountMember.IsActive)
            {
                lstAccountMemberOptions.Add(MemberOptionsCombinations[MemberType.Reactivate]);
            }

            if (accountMember.IsActive)
            {
                lstAccountMemberOptions.Add(MemberOptionsCombinations[MemberType.Deactivate]);
            }
			
			lstAccountMemberOptions.Add(MemberOptionsCombinations[MemberType.Remove]);

			return lstAccountMemberOptions;
        }
	}

	public class AccountMemberOptions
	{
		public MemberType MemberType { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public string ActionTitle { get; set; }
		public MvxCommandBase SubmitCommand { get; set; }
	}

	public enum MemberType
	{
		Private,
		Reactivate,
		Deactivate,
		Remove
	}
}
