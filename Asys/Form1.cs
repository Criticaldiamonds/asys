﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using System.Drawing.Text;
using System.IO;
namespace Asys
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }

        public formMain(String filePath)
        {
            InitializeComponent();
            addTab();
            silentOpen(filePath);
        }

        private int tabCount = 0;

        #region Methods
        #region Tabs
        private void addTab()
        {
            RichTextBox body = new RichTextBox();
            body.Name = "Body";
            body.Dock = DockStyle.Fill;
            body.ContextMenuStrip = contextMenuStrip1;
            body.AcceptsTab = true;
<<<<<<< HEAD
            body.Click += body_click;
=======
>>>>>>> cc4895d86b8d236edf65e941d9b6b47cdc7e341d

            TabPage t = new TabPage();
            tabCount += 1;

            String DocText = "Document " + tabCount;
            t.Name = DocText;
            t.Text = DocText;
            t.Controls.Add(body);

            tabControl1.TabPages.Add(t);
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
        }
<<<<<<< HEAD

=======
>>>>>>> cc4895d86b8d236edf65e941d9b6b47cdc7e341d
        private void removeTab()
        {
            if (tabControl1.TabPages.Count != 1)
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                tabCount -= 1;
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
            }
            else
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                tabCount -= 1;
                addTab();
            }
        }
        private void removeAllTabs()
        {
            foreach (TabPage p in tabControl1.TabPages)
            {
                tabControl1.TabPages.Remove(p);
                tabCount -= 1;
            }
            addTab();
        }
        private void removeAllTabsExceptThis()
        {
            foreach (TabPage p in tabControl1.TabPages)
            {
                if (p.Name != tabControl1.SelectedTab.Name)
                {
                    tabControl1.TabPages.Remove(p);
                    tabCount -= 1;
                }
            }
        }
        #endregion
        #region File Manipulation
        private void save()
        {
            saveFileDialog1.FileName = tabControl1.SelectedTab.Name;
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog1.Filter = "Plain Text Files|*.txt|Rich Text Format|*.rtf|C# Files|*.cs|Java Source Files|*.java|All Files|*.*";
            saveFileDialog1.Title = "Save";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileName.Length > 0)
                {
                    getCurrentDocument.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                }

                string fileName = Path.GetFileName(saveFileDialog1.FileName);
                tabControl1.SelectedTab.Text = fileName;
                tabControl1.SelectedTab.Name = fileName;

            }
        }
        private void saveAs()
        {
            saveFileDialog1.FileName = tabControl1.SelectedTab.Name;
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog1.Filter = "Plain Text Files|*.txt|Rich Text Format|*.rtf|C# Files|*.cs|Java Source Files|*.java|All Files|*.*";
            saveFileDialog1.Title = "Save As";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileName.Length > 0)
                {
                    getCurrentDocument.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                }

                string fileName = Path.GetFileName(saveFileDialog1.FileName);
                tabControl1.SelectedTab.Text = fileName;
                tabControl1.SelectedTab.Name = fileName;
            }
        }
        private void open()
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.Filter = "Plain Text Files|*.txt|Rich Text Format|*.rtf|C# Files|*.cs|Java Source Files|*.java|All Files|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName.Length > 0)
                {
                    string fileName = Path.GetFileName(openFileDialog1.FileName);
                    tabControl1.SelectedTab.Text = fileName;
                    tabControl1.SelectedTab.Name = fileName;

                    if (fileName.EndsWith(".rtf"))
                        getCurrentDocument.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    else
                        getCurrentDocument.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                }
            }
        }
        private void silentOpen(String filePath)
        {
            getCurrentDocument.AppendText(File.ReadAllText(filePath));
        }
        #endregion
        #region text
        private void undo()
        {
            getCurrentDocument.Undo();
        }
        private void redo()
        {
            getCurrentDocument.Redo();
        }
        private void cut()
        {
            getCurrentDocument.Cut();
        }
        private void copy()
        {
            getCurrentDocument.Copy();
        }
        private void paste()
        {
            getCurrentDocument.Paste();
        }
        private void selectAll()
        {
            getCurrentDocument.SelectAll();
        }
        #endregion
        #region other methods
        private void getFontCollection()
        {
            InstalledFontCollection ifonts = new InstalledFontCollection();
            foreach (FontFamily ff in ifonts.Families)
            {
                toolStripComboBox1.Items.Add(ff.Name);
            }
            toolStripComboBox1.SelectedIndex = 0;
            int i = 0;
            foreach (var item in toolStripComboBox1.Items)
            {
                i++;
                if (item.ToString().ToUpper().Equals("TIMES NEW ROMAN"))
                {
                    toolStripComboBox1.SelectedIndex = i - 1;
                    break;
                }
            }
        }
        private void setFontSizes()
        {
            for (int i = 0; i < 75; i++)
            {
                toolStripComboBox2.Items.Add(i);
            }
            toolStripComboBox2.SelectedIndex = 11; // 12pt
        }
        #endregion
        #endregion
        #region Properties
        private RichTextBox getCurrentDocument
        {
            get
            {
                return (RichTextBox)tabControl1.SelectedTab.Controls["Body"];
            }
        }
        #endregion
        #region events
        #region font
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            Font original = getCurrentDocument.SelectionFont;
            Font bold = null;
            if (original.Italic)
                bold = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold | FontStyle.Italic);
            else if (original.Underline)
                bold = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold | FontStyle.Underline);
            else if (original.Strikeout)
                bold = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold | FontStyle.Strikeout);
            else if (original.Italic && original.Underline)
                bold = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            else if (original.Italic && original.Strikeout)
                bold = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold | FontStyle.Italic | FontStyle.Strikeout);
            else if (original.Underline && original.Strikeout)
                bold = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold | FontStyle.Strikeout | FontStyle.Underline);
            else if (original.Italic && original.Strikeout && original.Underline)
                bold = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline | FontStyle.Strikeout);
            else
                bold = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold);

            Font reg = null;
            if (original.Italic)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Italic);
            else if (original.Underline)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Underline);
            else if (original.Strikeout)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Strikeout);
            else if (original.Italic && original.Underline)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Italic | FontStyle.Underline);
            else if (original.Italic && original.Strikeout)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Italic | FontStyle.Strikeout);
            else if (original.Underline && original.Strikeout)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Strikeout | FontStyle.Underline);
            else if (original.Italic && original.Strikeout && original.Underline)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Italic | FontStyle.Underline | FontStyle.Strikeout);
            else
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
=======
            Font bold = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold);
            Font reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
>>>>>>> cc4895d86b8d236edf65e941d9b6b47cdc7e341d

            if (getCurrentDocument.SelectionFont.Bold) { getCurrentDocument.SelectionFont = reg; }
            else { getCurrentDocument.SelectionFont = bold; }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            Font original = getCurrentDocument.SelectionFont;
            Font italic = null;
            if (original.Bold)
                italic = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic | FontStyle.Bold);
            else if (original.Underline)
                italic = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic | FontStyle.Underline);
            else if (original.Strikeout)
                italic = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic | FontStyle.Strikeout);
            else if (original.Bold && original.Underline)
                italic = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic | FontStyle.Bold | FontStyle.Underline);
            else if (original.Bold && original.Strikeout)
                italic = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic | FontStyle.Bold | FontStyle.Strikeout);
            else if (original.Underline && original.Strikeout)
                italic = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic | FontStyle.Strikeout | FontStyle.Underline);
            else if (original.Bold && original.Strikeout && original.Underline)
                italic = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic | FontStyle.Bold | FontStyle.Underline | FontStyle.Strikeout);
            else
                italic = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic);

            Font reg = null;
            if (original.Bold)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold);
            else if (original.Underline)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Underline);
            else if (original.Strikeout)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Strikeout);
            else if (original.Italic && original.Underline)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Underline);
            else if (original.Italic && original.Strikeout)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Strikeout);
            else if (original.Underline && original.Strikeout)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Strikeout | FontStyle.Underline);
            else if (original.Italic && original.Strikeout && original.Underline)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Underline | FontStyle.Strikeout);
            else
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
=======
            Font italic = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic);
            Font reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
>>>>>>> cc4895d86b8d236edf65e941d9b6b47cdc7e341d

            if (getCurrentDocument.SelectionFont.Italic) { getCurrentDocument.SelectionFont = reg; }
            else { getCurrentDocument.SelectionFont = italic; }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            Font original = getCurrentDocument.SelectionFont;
            Font under = null;
            if (original.Bold)
                under = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline | FontStyle.Bold);
            else if (original.Italic)
                under = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline | FontStyle.Italic);
            else if (original.Strikeout)
                under = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline | FontStyle.Strikeout);
            else if (original.Bold && original.Italic)
                under = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline | FontStyle.Bold | FontStyle.Underline);
            else if (original.Bold && original.Strikeout)
                under = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline | FontStyle.Bold | FontStyle.Strikeout);
            else if (original.Italic && original.Strikeout)
                under = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline | FontStyle.Strikeout | FontStyle.Italic);
            else if (original.Bold && original.Strikeout && original.Italic)
                under = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline | FontStyle.Bold | FontStyle.Italic | FontStyle.Strikeout);
            else
                under = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline);

            Font reg = null;
            if (original.Bold)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold);
            else if (original.Italic)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Italic);
            else if (original.Strikeout)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Strikeout);
            else if (original.Italic && original.Underline)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Underline);
            else if (original.Italic && original.Strikeout)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Strikeout);
            else if (original.Underline && original.Strikeout)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Strikeout | FontStyle.Underline);
            else if (original.Italic && original.Strikeout && original.Underline)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Underline | FontStyle.Strikeout);
            else
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
=======
            Font under = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline);
            Font reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
>>>>>>> cc4895d86b8d236edf65e941d9b6b47cdc7e341d

            if (getCurrentDocument.SelectionFont.Underline) { getCurrentDocument.SelectionFont = reg; }
            else { getCurrentDocument.SelectionFont = under; }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            Font original = getCurrentDocument.SelectionFont;
            Font strike = null;
            if (original.Bold)
                strike = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout | FontStyle.Bold);
            else if (original.Italic)
                strike = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout | FontStyle.Italic);
            else if (original.Underline)
                strike = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout | FontStyle.Underline);
            else if (original.Bold && original.Italic)
                strike = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout | FontStyle.Bold | FontStyle.Italic);
            else if (original.Bold && original.Underline)
                strike = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout | FontStyle.Bold | FontStyle.Underline);
            else if (original.Italic && original.Underline)
                strike = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout | FontStyle.Italic | FontStyle.Italic);
            else if (original.Bold && original.Underline && original.Italic)
                strike = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout | FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            else
                strike = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline);

            Font reg = null;
            if (original.Bold)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold);
            else if (original.Italic)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Italic);
            else if (original.Underline)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Underline);
            else if (original.Bold && original.Underline)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Underline);
            else if (original.Italic && original.Underline)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Italic | FontStyle.Underline);
            else if (original.Bold && original.Italic)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Italic);
            else if (original.Italic && original.Strikeout && original.Italic)
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
            else
                reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);

=======
            Font strike = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout);
            Font reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
>>>>>>> cc4895d86b8d236edf65e941d9b6b47cdc7e341d

            if (getCurrentDocument.SelectionFont.Strikeout) { getCurrentDocument.SelectionFont = reg; }
            else { getCurrentDocument.SelectionFont = strike; }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            getCurrentDocument.SelectedText = getCurrentDocument.SelectedText.ToUpper();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            getCurrentDocument.SelectedText = getCurrentDocument.SelectedText.ToLower();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            float px = getCurrentDocument.SelectionFont.SizeInPoints + 2;
            Font nf = new Font(getCurrentDocument.SelectionFont.Name, px, getCurrentDocument.SelectionFont.Style);

            getCurrentDocument.SelectionFont = nf;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            float px = getCurrentDocument.SelectionFont.SizeInPoints - 2;
            Font nf = new Font(getCurrentDocument.SelectionFont.Name, px, getCurrentDocument.SelectionFont.Style);

            getCurrentDocument.SelectionFont = nf;
        }
        #endregion
        #region color
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                getCurrentDocument.SelectionColor = colorDialog1.Color;
            }
        }

        private void HighlightGreen_Click(object sender, EventArgs e)
        {
            getCurrentDocument.SelectionBackColor = Color.Lime;
        }

        private void HighlightOrange_Click(object sender, EventArgs e)
        {
            getCurrentDocument.SelectionBackColor = Color.Orange;
        }

        private void HighlightYellow_Click(object sender, EventArgs e)
        {
            getCurrentDocument.SelectionBackColor = Color.Yellow;
        }

        private void HighlightWhite_Click(object sender, EventArgs e)
        {
            getCurrentDocument.SelectionBackColor = Color.White;
        }

        private void HighlightBlack_Click(object sender, EventArgs e)
        {
            getCurrentDocument.SelectionBackColor = Color.Black;
        }
        #endregion
        #region font type
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (getCurrentDocument.SelectionFont != null)
            {
                String t = toolStripComboBox1.ComboBox.GetItemText(toolStripComboBox1.ComboBox.SelectedItem);

                Font nf = new Font(t, getCurrentDocument.SelectionFont.Size, getCurrentDocument.SelectionFont.Style);
                getCurrentDocument.SelectionFont = nf;
            }
            else
            {
                String t = toolStripComboBox1.ComboBox.GetItemText(toolStripComboBox1.ComboBox.SelectedItem);
                getCurrentDocument.SelectionFont = new Font(t, Int16.Parse(toolStripComboBox2.SelectedIndex.ToString()), FontStyle.Regular);
            }
            
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            float px;
            float.TryParse(toolStripComboBox2.SelectedItem.ToString(), out px);
            Font nf = new Font(getCurrentDocument.SelectionFont.Name, px, getCurrentDocument.SelectionFont.Style);

            getCurrentDocument.SelectionFont = nf;
        }
        #endregion 
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Char count
            if (getCurrentDocument.Text.Length > 0)
            {
                toolStripStatusLabel1.Text = getCurrentDocument.Text.Length.ToString();
            }
            else
            {
                toolStripStatusLabel1.Text = "0";
            }
            // Word count
            char[] delims = new char[] { ' ', '\r', '\n' };
            toolStripStatusLabel3.Text = getCurrentDocument.Text.Split(delims, StringSplitOptions.RemoveEmptyEntries).Length.ToString();
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            addTab();
            getFontCollection();
            setFontSizes();

            var args = System.Environment.GetCommandLineArgs();
            var argPath = args.Skip(1).FirstOrDefault();
            if (!string.IsNullOrEmpty(argPath))
            {
                var fullPath = Path.GetFullPath(argPath);

                if (fullPath.EndsWith(".rtf"))
                    getCurrentDocument.LoadFile(fullPath, RichTextBoxStreamType.RichText);
                else
                    getCurrentDocument.LoadFile(fullPath, RichTextBoxStreamType.PlainText);

                tabControl1.SelectedTab.Text = Path.GetFileName(fullPath);
                tabControl1.SelectedTab.Name = Path.GetFileName(fullPath);
            }
        }
        #region menustrip
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectAll();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addTab();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAs();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
        #region toolstrip
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            addTab();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            open();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            save();
        }

        private void newToolStripButton_Click_1(object sender, EventArgs e)
        {
            addTab();
        }

        private void openToolStripButton_Click_1(object sender, EventArgs e)
        {
            open();
        }

        private void saveToolStripButton_Click_1(object sender, EventArgs e)
        {
            save();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            paste();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            removeTab();
        }
        #endregion
        #region contextmenu
        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            undo();
        }

        private void redoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            redo();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cut();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            paste();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            save();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeTab();
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeAllTabs();
        }

        private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeAllTabsExceptThis();
        }
        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutAsys().ShowDialog();
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult exit = MessageBox.Show("Are you sure you want to exit Asys?\nAny unsaved changes will be lost", "Asys", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (exit == DialogResult.Yes) 
            {
                ;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All Files|*.*";
            openFileDialog1.Multiselect = true;
            var clipdata = Clipboard.GetDataObject();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string fname in openFileDialog1.FileNames)
                {
                    var img = Image.FromFile(fname);
                    Clipboard.SetImage(img);
                    getCurrentDocument.Paste();
                }
            }
            Clipboard.SetDataObject(clipdata);
        }

        private void sidebarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sidebarToolStripMenuItem.Checked)
            {
                toolStrip1.Visible = true;
            }
            else
            {
                toolStrip1.Visible = false;
            }
        }

        private void toolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolbarToolStripMenuItem.Checked)
            {
                toolStrip2.Visible = true;
            }
            else
            {
                toolStrip2.Visible = false;
            }
        }
        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new AsysHelpDialog().Show();
        }

        private void formMain_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            new AsysHelpDialog().Show();
        }
        #endregion events

<<<<<<< HEAD
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            getCurrentDocument.SelectionAlignment = HorizontalAlignment.Left;
            toolStripButton11.Checked = false;
            toolStripButton12.Checked = false;
            toolStripButton13.Checked = true;
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            getCurrentDocument.SelectionAlignment = HorizontalAlignment.Center;
            toolStripButton11.Checked = false;
            toolStripButton12.Checked = true;
            toolStripButton13.Checked = false;
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            getCurrentDocument.SelectionAlignment = HorizontalAlignment.Right;
            toolStripButton11.Checked = true;
            toolStripButton12.Checked = false;
            toolStripButton13.Checked = false;
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (getCurrentDocument.SelectionBullet)
            {
                getCurrentDocument.SelectionBullet = false;
                toolStripButton14.Checked = false;
            }
            else
            {
                getCurrentDocument.SelectionBullet = true;
                toolStripButton14.Checked = true;
            }
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Input Text:", "Input", "", -1, -1);
            if (input != "")
            {
                float px = getCurrentDocument.SelectionFont.SizeInPoints - 2;
                Font nf = new Font(getCurrentDocument.SelectionFont.Name, px, getCurrentDocument.SelectionFont.Style);
                getCurrentDocument.SelectionFont = nf;

                getCurrentDocument.SelectionCharOffset = 10;
                getCurrentDocument.SelectedText = input;
                getCurrentDocument.SelectionCharOffset = 0;

                float px2 = getCurrentDocument.SelectionFont.SizeInPoints + 2;
                Font nf2 = new Font(getCurrentDocument.SelectionFont.Name, px, getCurrentDocument.SelectionFont.Style);
                getCurrentDocument.SelectionFont = nf2;
            }
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Input Text:", "Input", "", -1, -1);
            if (input != "")
            {
                float px = getCurrentDocument.SelectionFont.SizeInPoints - 2;
                Font nf = new Font(getCurrentDocument.SelectionFont.Name, px, getCurrentDocument.SelectionFont.Style);
                getCurrentDocument.SelectionFont = nf;

                getCurrentDocument.SelectionCharOffset = -10;
                getCurrentDocument.SelectedText = input;
                getCurrentDocument.SelectionCharOffset = 0;

                float px2 = getCurrentDocument.SelectionFont.SizeInPoints + 2;
                Font nf2 = new Font(getCurrentDocument.SelectionFont.Name, px, getCurrentDocument.SelectionFont.Style);
                getCurrentDocument.SelectionFont = nf2;
            }
        }

        // FONT MANAGEMENT
        void body_click(object sender, EventArgs e)
        {
            try
            {
                Font theFont = getCurrentDocument.SelectionFont;

                int fontName = 0;
                foreach (var item in toolStripComboBox1.Items)
                {
                    fontName++;
                    if (item.ToString().ToUpper().Equals(theFont.Name.ToUpper()))
                    {
                        toolStripComboBox1.SelectedIndex = fontName - 1;
                        break;
                    }
                }

                toolStripComboBox2.SelectedIndex = (int)theFont.SizeInPoints;

                if (theFont.Bold)
                    toolStripButton1.Checked = true;
                else
                    toolStripButton1.Checked = false;
                if (theFont.Italic)
                    toolStripButton2.Checked = true;
                else
                    toolStripButton2.Checked = false;
                if (theFont.Underline)
                    toolStripButton3.Checked = true;
                else
                    toolStripButton3.Checked = false;
                if (theFont.Strikeout)
                    toolStripButton4.Checked = true;
                else
                    toolStripButton4.Checked = false;

                if (getCurrentDocument.SelectionAlignment == HorizontalAlignment.Left)
                    toolStripButton13.Checked = true;
                else
                    toolStripButton13.Checked = false;
                if (getCurrentDocument.SelectionAlignment == HorizontalAlignment.Center)
                    toolStripButton12.Checked = true;
                else
                    toolStripButton12.Checked = false;
                if (getCurrentDocument.SelectionAlignment == HorizontalAlignment.Right)
                    toolStripButton11.Checked = true;
                else
                    toolStripButton11.Checked = false;
            }
            catch (Exception ex) { ; }
        }
=======

>>>>>>> cc4895d86b8d236edf65e941d9b6b47cdc7e341d
    }
}
