using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.RegularExpressions;

using AsysEditor.Classes;

namespace AsysEditor.Forms
{
    public partial class AsysDevMsg : Form
    {
        public AsysDevMsg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get the message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AsysDevMsg_Load(object sender, EventArgs e)
        {
            string msg = CurrentMessage;

            // replace [-n] with a newline
            // Functionality moved to XMLParser.Parse()
            // msg = Regex.Replace(msg, "\\[-n\\]", "\r\n");

            if (!msg.Equals(string.Empty))
            {
                txtMessage.Text = msg;
            }
        }

        public string CurrentMessage
        {
            get
            {
                return XMLParser.Parse(WebString.Load(Strings.AsysPrefs.Value), "devmsg", false, true);
            }
        }

        /// <summary>
        /// The '?' was clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Checking the box will prevent Asys from displaying this message again.\n" +
                            "It will not, however, prevent future messages from being displayed.", "Asys");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (chkDontShow.Checked)
            {
                Properties.Settings.Default.prefShowDevMsg = false;
                Properties.Settings.Default.sysPreviousDevMsg = txtMessage.Text;
                Properties.Settings.Default.Save();
            }
            this.Close();
        }
        
    }
}
