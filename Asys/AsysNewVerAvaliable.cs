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
using System.Diagnostics;

namespace Asys
{
    public partial class AsysNewVerAvaliable : Form
    {
        string oldver, newver;

        bool alreadyRunningInstaller = false;

        public AsysNewVerAvaliable(string o, string n)
        {
            InitializeComponent();
            this.oldver = o;
            this.newver = n;
        }

        private void AsysNewVerAvaliable_Load(object sender, EventArgs e)
        {
            lblCurVer.Text = String.Format("Current Version: {0}", oldver);
            lblNewVer.Text = String.Format("New Version: {0}", newver);
        }

        private void lnkDownloadNow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string downloadspath = KnownFolders.GetPath(KnownFolder.Downloads);

            btnWait.Enabled = false;
            lnkDownloadNow.Enabled = false;
            this.ControlBox = false;

            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileAsync(new System.Uri("https://github.com/Criticaldiamonds/asys/releases/download/" + newver + "/AsysInstaller.msi"),
                    downloadspath + @"\AsysInstaller.msi");
                progressBar1.Visible = true;
            }
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;

            if (progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
                if (checkBox1.Checked)
                {
                    Install();
                }
                else
                {
                    MessageBox.Show("Download Complete!\nInstaller is located in the Downloads folder",
                                    "Asys",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
        }

        void Install()
        {
            if (!alreadyRunningInstaller)
            {
                alreadyRunningInstaller = true;
                Process.Start(KnownFolders.GetPath(KnownFolder.Downloads) + @"\AsysInstaller.msi");
                Asys.SetShouldClose(true);
                Asys.console.SetShouldClose();
                Application.Exit();
            }
        }

        private void btnWait_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
