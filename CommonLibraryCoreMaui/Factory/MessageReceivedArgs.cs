using FM.LiveSwitch;
//using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using UIKit;

namespace CommonLibraryCoreMaui
{
    public class MessageReceivedArgs : EventArgs
    {
        public string Name { get; private set; }
        public string Message { get; private set; }

        public MessageReceivedArgs(string name, string message)
        {
            this.Name = name;
            this.Message = message;
        }
    }

    public class InputsAvailableArgs : EventArgs
    {
        public SourceInput[] Inputs { get; private set; }
        public Action1<SourceInput> SelectCallback { get; private set; }

        public InputsAvailableArgs(SourceInput[] inputs, Action1<SourceInput> selectCallback)
        {
            this.Inputs = inputs;
            this.SelectCallback = selectCallback;
        }
    }
}
