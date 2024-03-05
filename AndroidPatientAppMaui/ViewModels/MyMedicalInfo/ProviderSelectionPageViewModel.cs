using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.MyMedicalInfo
{
    public class ProviderSelectionPageViewModel : BaseViewModel
    {
        public ProviderSelectionPageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
                //BackCommand = new Command(BackAsync); 

                //Token = Preferences.Get("AuthToken", string.Empty);
                //PatientID = Preferences.Get("PatientID", 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
