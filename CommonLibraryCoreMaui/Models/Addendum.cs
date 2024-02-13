using System;

namespace CommonLibraryCoreMaui.Models
{
    public class Addendum : IEquatable<Addendum>
    {
        public int? AddendumID { get; set; }
        public int VisitID { get; set; }
        public int UserID { get; set; }
        public string ProviderName { get; set; }
        public string TimeEntered { get; set; }
        public string Text { get; set; }

		public bool Equals(Addendum other)
		{
			if (this.ProviderName != other.ProviderName) return false;
			if (this.Text != other.Text) return false;

			return true;
		}

		public Addendum ShallowCopy()
		{
			return (Addendum)this.MemberwiseClone();
		}
	}
}
