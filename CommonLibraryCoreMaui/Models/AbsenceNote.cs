using System;

namespace CommonLibraryCoreMaui.Models
{
    public class AbsenceNote : IEquatable<AbsenceNote>
	{
        public int? AbsenceNoteID { get; set; }
        public int? VisitID { get; set; }
        public string PatientName { get; set; }
        public string ProviderName { get; set; }
        public string RecipientName { get; set; }
        public string TimeEntered { get; set; }
        public string ReturnText { get; set; }
        public string RestrictionText { get; set; }
        public string Link { get; set; }
        public string Text { get; set; }
        public bool Other { get; set; }
        public string DateEntered { get; set; }

        public bool Equals(AbsenceNote other)
		{
			if (this.PatientName != other.PatientName) return false;
			if (this.ProviderName != other.ProviderName) return false;
            if (this.RecipientName != other.RecipientName) return false;
            if (this.ReturnText != other.ReturnText) return false;
			if (this.RestrictionText != other.RestrictionText) return false;
			if (this.Link != other.Link) return false;
			if (this.Text != other.Text) return false;
            if (this.Other != other.Other) return false;

			return true;
		}

		public AbsenceNote ShallowCopy()
		{
			return (AbsenceNote)this.MemberwiseClone();
		}
	}
}