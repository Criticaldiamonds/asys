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
                currentWord = Clean(currentWord);

                correction = spellCheck.Correct(currentWord);
            }

            txtMisspelledWord.Text = Clean(currentWord);
            txtCorrection.Text = correction;

            if (txtMisspelledWord.Text == String.Empty) MessageBox.Show("Finished scanning the document");
        }

        /// <summary>
        /// Removes excess characters from the input string
        /// </summary>
        /// <param name="?"></param>
        private string Clean(string input)
        {
            input = input.Trim();
            string cleanedString;
                                                            // Remove these characters
            cleanedString = Regex.Replace(input.ToLower(), "[_0123456789\"\'<>|\\/@#$%^&*()\\[\\]\\{\\}+=]", string.Empty);

            // Remove punctuation
            if (cleanedString.EndsWith(".") || cleanedString.EndsWith(",") || cleanedString.EndsWith(":") || cleanedString.EndsWith(";") ||
                cleanedString.EndsWith("!") || cleanedString.EndsWith("?") || cleanedString.EndsWith("\"") || cleanedString.EndsWith("\'"))
            {
                cleanedString = cleanedString.Substring(0, cleanedString.Length - 1);
            }

            // Remove prefixes
            if (cleanedString.StartsWith("\"") || cleanedString.StartsWith("\'"))
            {
                cleanedString = cleanedString.Substring(1);
            }

            return cleanedString;
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
