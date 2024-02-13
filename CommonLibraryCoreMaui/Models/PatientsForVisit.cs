using System;
using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public class Patient : ICloneable
    {
        public int? PatientID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName}".Trim();
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public string Text
        {
            get
            {
                return ToString();
            }
        }
    }

    public class PatientsForVisit
    {
        public List<Patient> Patients { get; set; }
        public List<Patient> Adults { get; set; }
    }
}
