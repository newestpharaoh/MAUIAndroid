namespace CommonLibraryCoreMaui.Models
{
    public class ICDCode
    {
        public int ID { get; set; }
        public string Value { get; set; }

        public ICDCode(int id, string value)
        {
            this.ID = id;
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
