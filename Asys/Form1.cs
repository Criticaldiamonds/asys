using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            TabPage t = new TabPage();
            tabCount += 1;

            String DocText = "Document " + tabCount;
            t.Name = DocText;
            t.Text = DocText;
            t.Controls.Add(body);

            tabControl1.TabPages.Add(t);
        }
        private void removeTab()
        {
            if (tabControl1.TabPages.Count != 1)
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                tabCount -= 1;
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
            saveFileDialog1.Filter = "RTF|*.rtf";
            saveFileDialog1.Title = "Save";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileName.Length > 0)
                {
                    getCurrentDocument.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                }
            }
        }
        private void saveAs()
        {
            saveFileDialog1.FileName = tabControl1.SelectedTab.Name;
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog1.Filter = "Plain Text Files|*.txt|C# Files|*.cs|Java Source Files|*.java|All Files|*.*";
            saveFileDialog1.Title = "Save As";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileName.Length > 0)
                {
                    getCurrentDocument.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                }
            }
        }
        private void open()
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.Filter = "Plain Text Files|*.txt|C# Files|*.cs|Java Source Files|*.java|All Files|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName.Length > 0)
                {
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
            Font bold = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold);
            Font reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);

            if (getCurrentDocument.SelectionFont.Bold) { getCurrentDocument.SelectionFont = reg; }
            else { getCurrentDocument.SelectionFont = bold; }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Font italic = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic);
            Font reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);

            if (getCurrentDocument.SelectionFont.Italic) { getCurrentDocument.SelectionFont = reg; }
            else { getCurrentDocument.SelectionFont = italic; }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Font under = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline);
            Font reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);

            if (getCurrentDocument.SelectionFont.Underline) { getCurrentDocument.SelectionFont = reg; }
            else { getCurrentDocument.SelectionFont = under; }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Font strike = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout);
            Font reg = new Font(getCurrentDocument.SelectionFont.FontFamily, getCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);

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
            if (getCurrentDocument.Text.Length > 0)
            {
                toolStripStatusLabel1.Text = getCurrentDocument.Text.Length.ToString();
            }
            else
            {
                toolStripStatusLabel1.Text = "0";
            }
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            string[] args = System.Environment.GetCommandLineArgs();
            string filePath = args[1];
            //filePath.Replace("\\\\", "\\");
            addTab();
            getFontCollection();
            setFontSizes();
            //getCurrentDocument.Text = (File.ReadAllText(filePath));

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
        #endregion events
    }
}
