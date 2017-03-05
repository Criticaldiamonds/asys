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
        bool force;

        public void Start(string curv, bool force)
        {
            this.force = force;


            string newv = XMLParser.Parse(WebString.Load(Strings.AsysPrefs.Value), "version", true, false);
            
            if (!newv.Equals(string.Empty))
            {
                ProcessVersion(curv, newv);
            }
        }

        private void ProcessVersion(string curVer, string newVer)
        {

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
                        // If the user skipped the previous update
                        if (Properties.Settings.Default.prefSkipUpdate)
                        {
                            // If this update isn't the one the user skipped
                            if (Properties.Settings.Default.sysSkippedVersion != newVer)
                            {

                                Asys.console.Append(Asys.GetTime() + "Update Found! Showing update dialog");
                                update = true;
                                new AsysNewVerAvaliable(curVer, newVer).ShowDialog();
                                break;
                            }
                        }
                        // The user didn't skip any updates
                        else
                        {
                            Asys.console.Append(Asys.GetTime() + "Update Found! Showing update dialog");
                            update = true;
                            new AsysNewVerAvaliable(curVer, newVer).ShowDialog();
                            break;
                        }
                    }
                }
                if (!update)
                {
                    Asys.console.Append(Asys.GetTime() + "No Update Found");
                    if (force) System.Windows.Forms.MessageBox.Show("No Update Found!");
                }
            }
        }

    }
}
