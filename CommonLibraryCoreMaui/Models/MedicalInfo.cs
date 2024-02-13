using System.Collections.Generic;
using System.Linq;

namespace CommonLibraryCoreMaui.Models
{
    public class MedicalInfo
    {
        public MedicalInfo()
        {
            Allergies = new List<Allergy>();
            Medications = new List<Medication>();
            Surgeries = new List<Surgery>();
            MedicalIssues = new List<int>();
        }

        public string AllergiesString => string.Join(", ", Allergies.Select(x => x.Name));
        public string MedicationsString => string.Join(", ", Medications.Select(x => x.Name));
        public string SurgeriesString => string.Join(", ", Surgeries.Select(x => x.Name));

        public List<int> MedicalIssues { get; set; }
        public List<Allergy> Allergies { get; set; }
        public List<Medication> Medications { get; set; }
        public List<Surgery> Surgeries { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public PCP PCP { get; set; }
        public int PatientID { get; set; }
        public string OtherMedicalIssue { get; set; }

        public bool IsNotProvided()
        {
            return Pharmacy.IsNotProvided()
                && PCP.IsNotProvided()
                && OtherMedicalIssue is null 
                && Allergies.Count == 0 
                && Medications.Count == 0 
                && Surgeries.Count == 0 
                && MedicalIssues.Count == 0;
        }
    }
}