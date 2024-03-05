using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.Family
{
    public class FamilyPageViewModel :BaseViewModel
    {

        #region Constructor
        public FamilyPageViewModel(INavigation nav)
        {
            try
            {
                Navigation = nav;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion

        #region Command
        #endregion

        #region Properties
        #endregion

        #region Methods
        #endregion
    }
}
