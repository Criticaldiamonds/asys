using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AsysEditor.Forms;

namespace AsysEditor.Classes
{
    class EventHandler
    {
        private Asys asys;
        
        public EventHandler(Asys asysIn)
        {
            asys = asysIn;
        }

        public string Handle(string event_)
        {
            if (event_.Equals(String.Empty)) return "error";
            event_ = event_.ToLower(); // Lowercase the imput

            string result = "done";

            switch (event_) {
                case "asys.updateformat":
                    asys_updateformat();
                    break;
                default:
                    result = "error: " + event_ + " is not a valid event";
                    break;
            }

            return result;
        }

        private void asys_updateformat()
        {
            if (asys.documentTab.SelectedTab.Name.StartsWith("asysdefault_")) {
                asys.toolLblFormat.Text = "Rich Text";
                return;
            }

            int id = int.Parse(asys.documentTab.SelectedTab.Name);

            switch (asys.fileInteraction.GetFileType(id)) {
                case FileType.PlainText:
                    asys.toolLblFormat.Text = "Plain Text";
                    break;
                case FileType.RichText:
                    asys.toolLblFormat.Text = "Rich Text";
                    break;
                default:
                    asys.toolLblFormat.Text = "Unspecified";
                    break;
            }
        }

    }
}
