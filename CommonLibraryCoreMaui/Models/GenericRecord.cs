namespace CommonLibraryCoreMaui.Models
{
    public class GenericRecord
    {
        public int? ID { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }

        public string Text
        {
            get
            {
                return ToString();
            }
        }
    }
}