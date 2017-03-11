using System.Windows.Forms;

namespace AsysEditor.Forms
{
    public partial class AsysHelpDialog : Form
    {
        public AsysHelpDialog()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new AsysAbout().ShowDialog();
        }
    }
}
