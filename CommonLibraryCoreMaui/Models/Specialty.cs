using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public class Specialty
    {
        public int ID { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return this.Value;
        }
    }

    public class SpecialtiesResponse
    {
        public List<Specialty> Specialties { get; set; }
    }
}
