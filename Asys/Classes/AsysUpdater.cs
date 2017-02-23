using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AsysEditor.Forms;

namespace AsysEditor.Classes
{
    class AsysUpdater
    {
        private string a_prefs_url = "https://dl.dropboxusercontent.com/u/276558657/Asys/asys.a_prefs";

        public void Start(string curv)
        {
            AsysExternalStringParser aesp = new AsysExternalStringParser();
            aesp.Load(a_prefs_url);
            string newv = aesp.GetKey("VERSION");

            if (!(newv == ""))
            {
                ProcessVersion(curv, newv);
            }
        }

        private void ProcessVersion(string curVer, string newVer)
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
