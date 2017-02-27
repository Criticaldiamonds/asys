using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;

namespace AsysEditor.Forms
{
    public partial class AsysChangelogViewer : Form
    {
        public AsysChangelogViewer(string url)
        {
            InitializeComponent();
            string text = "An error occured while retrieving the changelog";
            try
            {
                using (WebClient client = new WebClient())
                {
                    text = client.DownloadString(url);
                }
            }
            catch (Exception)
            {
                Asys.console.Append(Asys.GetTime() + "[ERROR]: AsysChangelogViewer: Could not load!");
            }

            richTextBoxPrintCtrl1.Rtf = text;
        }
    }
}
