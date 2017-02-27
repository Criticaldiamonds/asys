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
using AsysControls;

namespace AsysEditor.Forms
{
    public partial class AsysSpellCheck : Form
    {
        /// <summary>Implementation of the Spell Checker</summary>
        private SpellCheck spellCheck;

        Queue<string> queue = new Queue<string>();

        RichTextBoxPrintCtrl rtb;

        public AsysSpellCheck(RichTextBoxPrintCtrl rtbIn)
        {
            InitializeComponent();

            this.rtb = rtbIn;
        }

        private void AsysSpellCheck_Load(object sender, EventArgs e)
        {
            
        }

        private void refresh()
        {
            queue.Clear();
            string input = rtb.Text;
            foreach (string item in input.Split(' ', '\n'))
            {
                queue.Enqueue(item);
            }

            next();
        }

        private void next()
        {
            string currentWord = String.Empty;
            string correction = String.Empty;
            
            while (correction.Equals(currentWord, StringComparison.InvariantCultureIgnoreCase))
            {
                if (queue.Count <= 0) break;

                currentWord = queue.Dequeue();
                currentWord = Regex.Replace(currentWord.ToLower(), @"[\W_]", string.Empty);
                correction = spellCheck.Correct(currentWord);
            }

            txtMisspelledWord.Text = currentWord;
            txtCorrection.Text = correction;

            if (txtMisspelledWord.Text == String.Empty) MessageBox.Show("Finished scanning the document");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            next();
        }

        private void AsysSpellCheck_Shown(object sender, EventArgs e)
        {
            
            spellCheck = new SpellCheck();
            spellCheck.init();
            lblLoading.Visible = false;
            refresh();
        }
    }
}
