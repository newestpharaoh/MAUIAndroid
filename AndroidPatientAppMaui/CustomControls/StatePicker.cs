using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidPatientAppMaui.CustomControls
{
    public class StatePicker : Picker
    {
        List<string> states = CommonLibraryCoreMaui.Theme.Values.States;
            // Populate the list of states here
            //"Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware",
            //"Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky",
            //"Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi",
            //"Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico",
            //"New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania",
            //"Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont",
            //"Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming"
        

        public StatePicker()
        {
            // Populate the Picker with states
            foreach (string state in states)
            {
                Items.Add(state);
            }

            // Select TX by default
            SelectState("Texas");
        }

        public void SelectState(string state)
        {
            int index = states.FindIndex(x => x.Equals(state));
            if (index != -1)
            {
                SelectedIndex = index;
            }
        }
    }
}
