﻿using System;
using System.Windows.Forms;

namespace AsysEditor.Forms
{
    public partial class AsysSingleTabCloseHandler : Form
    {
        public AsysSingleTabCloseHandler()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Asys.SetShouldClose(true);
            Asys.console.SetShouldClose();
            Application.Exit();
        }

    }
}
