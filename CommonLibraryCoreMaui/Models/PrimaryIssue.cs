using System;
using MvvmCross.Commands;

namespace CommonLibraryCoreMaui.Models
{
	public class PrimaryIssue : ICloneable, IPatientRegistrationMedicalInfoListItem
	{
		public string IssueId { get; set; }
		public PrimaryIssueType IssueType { get; set; }
		public IssueActionType ActionType { get; private set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public string HeaderTitle { get; set; }
		public string NameTitle { get; set; }
		public string DescriptionTitle { get; set; }
		public IMvxCommand EditCommand { get; set; }
		public IMvxCommand DeleteCommand { get; set; }

		public string ListCaption { get { return Name; } }
		public int Position { get; set; }

		public void SetLabelTitles()
		{
			var issue = MedicalIssueFactory.Get(IssueType);
			HeaderTitle = issue.HeaderTitle;
			NameTitle = issue.NameTitle;
			DescriptionTitle = issue.DescriptionTitle;
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

		public void SetAction(IssueActionType issueActionType)
		{
			this.ActionType = issueActionType;
			if (ActionType == IssueActionType.Add)
			{
				HeaderTitle = $"Add {IssueType.ToString()}";
			}
			else if (ActionType == IssueActionType.Edit)
			{
				HeaderTitle = $"Edit {IssueType.ToString()}";
			}
		}
	}

	public enum PrimaryIssueType
	{
		Allergy,
		Medication,
		Surgery,
		Pharmacy,
		PCP
	}

	public enum IssueActionType
	{
		Add,
		Edit,
		Delete
	}
}
