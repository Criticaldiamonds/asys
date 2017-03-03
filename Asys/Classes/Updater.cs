using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AsysEditor.Forms;

namespace AsysEditor.Classes
{
    class Updater
    {
        public void Start(string curv)
        {
            ExternalStringParser esp = new ExternalStringParser();
            esp.Load(EStrings.AsysPrefs_NEW.ToString());
            string newv = esp.ParseKey("version");

            if (!newv.Equals(string.Empty))
            {
                ProcessVersion(curv, newv);
            }
        }

        private void ProcessVersion(string curVer, string newVer)
        {
            // For testing only newVer = "10.0.0";

            if (newVer.Equals("error"))
            {
                System.Windows.Forms.MessageBox.Show("Unable to connect to update site", "Asys Updater");
            }
            else
            {
                string[] curVersion = curVer.Split('.');
                string[] newVersion = newVer.Split('.');

                int[] curv = curVersion.Select(int.Parse).ToArray();
                int[] newv = newVersion.Select(int.Parse).ToArray();

                bool update = false;

                for (int i = 0; i < curv.Length; i++)
                {
                    if (newv[i] > curv[i])
                    {
                        Asys.console.Append(Asys.GetTime() + "Update Found! Showing update dialog");
                        update = true;
                        new AsysNewVerAvaliable(curVer, newVer).ShowDialog();
                        break;
                    }
                }
                if (!update)
                {
                    Asys.console.Append(Asys.GetTime() + "No Update Found");
                }
            }
        }

    }
}
