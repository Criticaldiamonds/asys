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
    public partial class AsysSingleTabCloseHandler : Form
    {
        Asys frm;
        AsysConsole console;
        public AsysSingleTabCloseHandler(Asys frm, AsysConsole console)
        {
            this.frm = frm;
            this.console = console;
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm.SetShouldClose(true);
            console.SetShouldClose();
            Application.Exit();
        }

    }
}
