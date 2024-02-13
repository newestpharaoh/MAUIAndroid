using System;

namespace CommonLibraryCoreMaui.Models
{
    public class UIText
    {
        public String TagName;                          // unique identifier for the text -- used by the UI to find text to be displayed
        public String Text;                             // english or spanish text to display in the UI (set based on locale sent in REST GET)
    }
}