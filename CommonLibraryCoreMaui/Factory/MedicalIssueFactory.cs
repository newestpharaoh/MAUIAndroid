using System.Collections.Generic;
using CommonLibraryCoreMaui.Models;

namespace CommonLibraryCoreMaui
{
	public static class MedicalIssueFactory
	{
		static Dictionary<PrimaryIssueType, PrimaryIssue> MedicalIssueCombination;
		static MedicalIssueFactory()
		{
			MedicalIssueCombination = new Dictionary<PrimaryIssueType, PrimaryIssue>(3)
			{
				{
					PrimaryIssueType.Allergy,
					new Allergy()
					{
						HeaderTitle = "Add Allergy",
						NameTitle = "Allergy Name*",
						DescriptionTitle = "Include type of reaction (ex: Throat swelling, hives, etc.) and severity"
					}
				},
				{
					PrimaryIssueType.Medication,
					new Medication()
					{
						HeaderTitle = "Add Medication",
						NameTitle = "Medication Name*",
						DescriptionTitle ="Please include dosage and frequency. Ex. 10mg once per day"
					}
				},
				{
					PrimaryIssueType.Surgery,
					new Surgery()
					{
						HeaderTitle = "Add Surgery",
						NameTitle = "Surgery Name*",
						DescriptionTitle = "Please include information about surgery type, organ or part of body and date"
					}
				}
			};
		}

		public static PrimaryIssue Get(PrimaryIssueType issueType)
		{
			return MedicalIssueCombination[issueType];
		}

		public static PrimaryIssue GetIssueTitlesOnly(PrimaryIssueType issueType)
		{
			var issue = MedicalIssueCombination[issueType];
			issue.Name = issue.Description = string.Empty;
			return issue;
		}
	}
}
