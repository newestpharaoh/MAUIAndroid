﻿using System;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
