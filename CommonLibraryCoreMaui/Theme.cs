using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLibraryCoreMaui
{
    public static class Theme
    {

        public static class Colors
        {
            public static string GrayButtonColor = "#AAAAAA";
            public static string HyperLinkBlueColor = "#14B38A";
            public static string VeryLightGrayColor = "#e0e0eb";

        }

        public static class Fonts
        {

        }
        public static class Values
        {
            public static List<string> GenderOptions = new List<string> {
                "Select",
                "Female",
                "Male",
                "Other",
                "Decline"
            };

            public static List<CommonLibraryCoreMaui.Models.GenericRecord> Relationships = new List<CommonLibraryCoreMaui.Models.GenericRecord>() {
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = -1, Value =  "Select" },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 1, Value =  "Aunt" },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 2, Value =  "Brother" },
                 new CommonLibraryCoreMaui.Models.GenericRecord(){  ID = 18, Value = "Caregiver" },

                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 3, Value =  "Cousin" },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 4, Value =  "Daughter" },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 5, Value =  "Father" },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 6, Value =  "Foster Child"  },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 7, Value =  "Grandfather" },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 8, Value =  "Grandmother"  },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 9, Value =  "Legal Dependent"  },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 10, Value =  "Mother"  },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 11, Value =  "Nephew"  },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 12, Value =  "Niece"  },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 13, Value =  "Partner/Significant Other"  },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 14, Value =  "Sister"  },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 15, Value =  "Son"  },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 16, Value =  "Spouse"  },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  ID = 17, Value =  "Uncle"  },
                new CommonLibraryCoreMaui.Models.GenericRecord() {  Value =  "Other (Write in Box Below)"  }
            };

            public static string phoneMain = "512-421-5678";
            public static string phoneMainURL = "tel:15124215678";

            public static List<string> VisitReasons = new List<string> {
                "Allergy Symptoms",
                "Asthma Symptoms",
                "Cold and Flu Symptoms",
                "Cough",
                "Earache and Swimmer\'s Ear",
                "GERD or Heartburn Symptoms",
                "Gout",
                "Insect Bite or Sting",
                "Joint Pain",
                "Minor Back or Shoulder Pain",
                "Minor Injury, Sprain, or Strain",
                "Minor Trauma, Burn, or Laceration",
                "Nausea and Diarrhea",
                "Pink Eye and Eye Irritation",
                "Sinus Infection Symptoms",
                "Skin Rash or Minor Skin Infection",
                "Sore Throat or Tonsillitis Symptoms",
                "Urinary or UTI Symptoms",
                "Yeast Infection",
                "Other (Please briefly describe your issue/concern or question)"
            };
            public static List<string> States = new List<string>
            {
                "AL",
                "AR",
                "AZ",
                "CA",
                "CO",
                "CT",
                "DE",
                "DC",
                "FL",
                "GA",
                "HI",
                "ID",
                "IL",
                "IN",
                "IA",
                "KS",
                "KY",
                "LA",
                "ME",
                "MD",
                "MA",
                "MI",
                "MN",
                "MS",
                "MO",
                "MT",
                "NE",
                "NV",
                "NH",
                "NJ",
                "NM",
                "NY",
                "NC",
                "ND",
                "OH",
                "OK",
                "OR",
                "PA",
                "RI",
                "SC",
                "SD",
                "TN",
                "TX",
                "UT",
                "VT",
                "VA",
                "WA",
                "WV",
                "WI",
                "WY"
            };
            public static List<string> RepeatAlerts = new List<string> {
                "Never",
                "Once",
                "Twice",
                "3 Times",
                "4 Times",
                "5 Times"
            };
            public static List<string> PaymentPlans = new List<string> {
                "72-Hour Access - $40",
                "Individual Subscription - $9.95/mo.",
                "Family Subscription - $19.95/mo."
            };
            public static List<string> UserTitles = new List<string> {
                "Select",
                "Mr.",
                "Ms.",
                "Mrs.",
                "Miss"
            };
            public static List<string> MonthsNumeric = new List<string> {
                "1","2","3","4","5","6","7","8","9","10","11","12"
            };
            public static List<string> CCYears = new List<string>(Enumerable.Range(DateTime.Now.Year, 10).ToList().ConvertAll<string>(x => x.ToString().Substring(2, 2)));
            public static List<string> Languages = new List<string> {
                "English",
                "Spanish"
            };
        }
    }
}
