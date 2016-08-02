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
    public partial class AsysCloseHandler : Form
    {
        int count;
        formMain frm;
        public AsysCloseHandler(int count, formMain frm)
        {
            this.count = count;
            this.frm = frm;
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm.removeTab();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm.setShouldClose(true);
            Application.Exit();
        }

        private void AsysCloseHandler_Load(object sender, EventArgs e)
        {
            label1.Text = "You are about to close " + count + " tabs.\n"+
                          "Do you want to close all tabs,\n"+
                          "or just the current tab?";
        }
    }
}
