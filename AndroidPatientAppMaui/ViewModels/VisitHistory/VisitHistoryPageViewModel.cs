using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.ViewModels.VisitHistory
{
    public class VisitHistoryPageViewModel :BaseViewModel
    {
        #region Constructor
        public VisitHistoryPageViewModel(INavigation nav)
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
