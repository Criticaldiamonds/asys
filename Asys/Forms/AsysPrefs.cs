using System;
using System.Windows.Forms;

namespace AsysEditor.Forms
{
    public partial class AsysPrefs : Form
    {
        public AsysPrefs()
        {
            InitializeComponent();
        }

        private void AsysPrefs_Load(object sender, EventArgs e)
        {
            chkWelcome.Checked = Properties.Settings.Default.prefShowWelcome;
            chkChangelog.Checked = Properties.Settings.Default.prefShowChangelog;
            chkWinSizeLoc.Checked = Properties.Settings.Default.prefSaveLoc;
            chkDisableUpdater.Checked = Properties.Settings.Default.prefDisableAutoUpdate;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (chkWelcome.Checked)
                Properties.Settings.Default.prefShowWelcome = true;
            else
                Properties.Settings.Default.prefShowWelcome = false;

            if (chkChangelog.Checked)
                Properties.Settings.Default.prefShowChangelog = true;
            else
                Properties.Settings.Default.prefShowChangelog = false;

            if (chkWinSizeLoc.Checked)
                Properties.Settings.Default.prefSaveLoc = true;
            else
                Properties.Settings.Default.prefSaveLoc = false;

            if (chkDisableUpdater.Checked)
                Properties.Settings.Default.prefDisableAutoUpdate = true;
            else
                Properties.Settings.Default.prefDisableAutoUpdate = false;

            Properties.Settings.Default.Save();
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Asys.LaunchUpdater(true);
        }
    }
}
