using System;

namespace CommonLibraryCoreMaui.Models
{
    public class UIText
    {
        public string TagName { get; set; }
        public string Text { get; set; }
        //public string TagName;                          // unique identifier for the text -- used by the UI to find text to be displayed
        //public string Text;                             // english or spanish text to display in the UI (set based on locale sent in REST GET)
    }
}