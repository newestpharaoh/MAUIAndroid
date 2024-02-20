using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace CommonLibraryCoreMaui.Models
{
    public class UITopic
    {
        public string TopicName { get; set; }
        public List<UIText> UITextList { get; set; }
        //public string TopicName;										// identifies the group of UI Text items 
        //public List<UIText> UITextList = new List<UIText>();		// list of the UI text items associated with 

    }
}
