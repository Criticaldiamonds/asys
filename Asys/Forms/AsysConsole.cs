using System.Windows.Forms;

namespace AsysEditor.Forms
{
    public partial class AsysConsole : Form
    {
        bool close = false;

        public AsysConsole()
        {
            InitializeComponent();
        }

        public void Append(string StringIn)
        {
            textBox1.AppendText(StringIn + "\n");
        }

        public void SetShouldClose()
        {
            close = true;
        }

        private void AsysConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!close) {
                e.Cancel = true;
                this.Hide();
            }
            else
                e.Cancel = false;
        }
    }
}
