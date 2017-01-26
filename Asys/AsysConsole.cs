using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asys
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
            if (!close)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
                e.Cancel = false;
        }
    }
}
