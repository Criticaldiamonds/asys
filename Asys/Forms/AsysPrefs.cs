using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void button1_Click(object sender, EventArgs e)
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

            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
