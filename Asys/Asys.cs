using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;

using System.Reflection;

using RichTextBoxPrintCtrl;
namespace Asys
{
    public partial class Asys : Form
    {
        private FileInteraction fileInteraction;
        public static AsysConsole console;

        private string a_prefs_url = "https://dl.dropboxusercontent.com/u/276558657/Asys/asys.a_prefs";

        public Asys()
        {
            Init();
        }

        public Asys(String filePath)
        {
            Init();
            AddTab();
            string fileName = fileInteraction.silentOpen(GetCurrentDocument, filePath);
            documentTab.SelectedTab.Text = filePath;
        }

        private void Init()
        {
            InitializeComponent();

            console = new AsysConsole();
            console.Show();
            console.Hide();
            console.Append(GetTime() + "Asys Started");

            fileInteraction = new FileInteraction();
            fileInteraction.Init(console);

            sidebarToolStripMenuItem.Checked = Properties.Settings.Default.prefShowSidebar;
            toolStrip1.Visible = Properties.Settings.Default.prefShowSidebar;
            toolbarToolStripMenuItem.Checked = Properties.Settings.Default.prefShowToolbar;
            toolStrip2.Visible = Properties.Settings.Default.prefShowToolbar;
            statusbarToolStripMenuItem.Checked = Properties.Settings.Default.prefShowStatusbar;
            statusStrip1.Visible = Properties.Settings.Default.prefShowStatusbar;
            console.Append(GetTime() + "Initialization complete");
        }

        private int tabCount = 0;
        private int checkPrint;

        #region Methods
        #region Tabs
        private void AddTab()
        {
            RichTextBoxPrintCtrl.RichTextBoxPrintCtrl body = new RichTextBoxPrintCtrl.RichTextBoxPrintCtrl();
            body.Name = "Body";
            body.Dock = DockStyle.Fill;
            body.ContextMenuStrip = contextMenuStrip1;
            body.AcceptsTab = true;
            body.SelectionChanged += body_SelectionChanged;

            TabPage t = new TabPage();
            tabCount += 1;

            String DocText = "Document " + tabCount;
            t.Name = DocText;
            t.Text = DocText;
            t.Controls.Add(body);

            documentTab.TabPages.Add(t);
            documentTab.SelectedIndex = documentTab.TabPages.Count - 1;
            console.Append(GetTime() + "Tab successfully added");
        }
        public void RemoveTab()
        {
            if (documentTab.TabPages.Count != 1)
            {
                fileInteraction.RemoveFileFromMap(documentTab.SelectedTab.Name);
                documentTab.TabPages.Remove(documentTab.SelectedTab);
                tabCount -= 1;
                documentTab.SelectedIndex = documentTab.TabPages.Count - 1;
                console.Append(GetTime() + "Tab successfully removed");
            }
            else
            {
                fileInteraction.RemoveFileFromMap(documentTab.SelectedTab.Name);
                documentTab.TabPages.Remove(documentTab.SelectedTab);
                tabCount -= 1;
                console.Append(GetTime() + "Tab successfully removed");
                AddTab();
            }
        }
        private void RemoveAllTabs()
        {
            foreach (TabPage p in documentTab.TabPages)
            {
                documentTab.TabPages.Remove(p);
                tabCount -= 1;
            }
            AddTab();
        }
        private void RemoveAllTabsExceptThis()
        {
            foreach (TabPage p in documentTab.TabPages)
            {
                if (p.Name != documentTab.SelectedTab.Name)
                {
                    documentTab.TabPages.Remove(p);
                    tabCount -= 1;
                }
            }
        }
        #endregion        
        
        #region text
        private void Undo()
        {
            GetCurrentDocument.Undo();
        }
        private void Redo()
        {
            GetCurrentDocument.Redo();
        }
        private void Cut()
        {
            GetCurrentDocument.Cut();
        }
        private void Copy()
        {
            GetCurrentDocument.Copy();
        }
        private void Paste()
        {
            GetCurrentDocument.Paste();
        }
        private void SelectAll()
        {
            GetCurrentDocument.SelectAll();
        }
        private void Print()
        {
            // new testv().Show();
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
        private void PrintPreview()
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }
        private void PageSetup()
        {
            pageSetupDialog1.Document = printDocument1;
            pageSetupDialog1.PageSettings = new System.Drawing.Printing.PageSettings();
            pageSetupDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            pageSetupDialog1.ShowNetwork = false;

            DialogResult result = pageSetupDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument1.DefaultPageSettings = pageSetupDialog1.PageSettings;
                printDocument1.PrinterSettings = pageSetupDialog1.PrinterSettings;
            }
        }
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            checkPrint = 0;
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Print the content of RichTextBox. Store the last character printed.
            checkPrint = GetCurrentDocument.Print(checkPrint, GetCurrentDocument.TextLength, e);

            // Check for more pages
            if (checkPrint < GetCurrentDocument.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }
        #endregion
        #region other methods
        private void GetFontCollection()
        {
            InstalledFontCollection ifonts = new InstalledFontCollection();
            foreach (FontFamily ff in ifonts.Families)
            {
                fontToolStripComboBox.Items.Add(ff.Name);
            }
            fontToolStripComboBox.SelectedIndex = 0;
            int i = 0;
            foreach (var item in fontToolStripComboBox.Items)
            {
                i++;
                if (item.ToString().ToUpper().Equals("TIMES NEW ROMAN"))
                {
                    fontToolStripComboBox.SelectedIndex = i - 1;
                    break;
                }
            }
            console.Append(GetTime() + "Successfully loaded FontCollection");
        }
        private void SetFontSizes()
        {
            for (int i = 0; i < 75; i++)
            {
                fontSizeToolStripComboBox.Items.Add(i);
            }
            fontSizeToolStripComboBox.SelectedIndex = 12; // 12pt
        }
        #endregion
        #endregion
        #region Properties
        private RichTextBoxPrintCtrl.RichTextBoxPrintCtrl GetCurrentDocument
        {
            get
            {
                return (RichTextBoxPrintCtrl.RichTextBoxPrintCtrl)documentTab.SelectedTab.Controls["Body"];
            }
        }
        #endregion
        #region events
        #region font
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Font original = GetCurrentDocument.SelectionFont;
            Font bold = null;
            if (original.Italic)
                bold = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold | FontStyle.Italic);
            else if (original.Underline)
                bold = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold | FontStyle.Underline);
            else if (original.Strikeout)
                bold = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold | FontStyle.Strikeout);
            else if (original.Italic && original.Underline)
                bold = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            else if (original.Italic && original.Strikeout)
                bold = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold | FontStyle.Italic | FontStyle.Strikeout);
            else if (original.Underline && original.Strikeout)
                bold = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold | FontStyle.Strikeout | FontStyle.Underline);
            else if (original.Italic && original.Strikeout && original.Underline)
                bold = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline | FontStyle.Strikeout);
            else
                bold = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Bold);

            Font reg = null;
            if (original.Italic)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Italic);
            else if (original.Underline)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Underline);
            else if (original.Strikeout)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Strikeout);
            else if (original.Italic && original.Underline)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Italic | FontStyle.Underline);
            else if (original.Italic && original.Strikeout)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Italic | FontStyle.Strikeout);
            else if (original.Underline && original.Strikeout)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Strikeout | FontStyle.Underline);
            else if (original.Italic && original.Strikeout && original.Underline)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Italic | FontStyle.Underline | FontStyle.Strikeout);
            else
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);

            if (GetCurrentDocument.SelectionFont.Bold) { GetCurrentDocument.SelectionFont = reg; }
            else { GetCurrentDocument.SelectionFont = bold; }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Font original = GetCurrentDocument.SelectionFont;
            Font italic = null;
            if (original.Bold)
                italic = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic | FontStyle.Bold);
            else if (original.Underline)
                italic = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic | FontStyle.Underline);
            else if (original.Strikeout)
                italic = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic | FontStyle.Strikeout);
            else if (original.Bold && original.Underline)
                italic = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic | FontStyle.Bold | FontStyle.Underline);
            else if (original.Bold && original.Strikeout)
                italic = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic | FontStyle.Bold | FontStyle.Strikeout);
            else if (original.Underline && original.Strikeout)
                italic = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic | FontStyle.Strikeout | FontStyle.Underline);
            else if (original.Bold && original.Strikeout && original.Underline)
                italic = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic | FontStyle.Bold | FontStyle.Underline | FontStyle.Strikeout);
            else
                italic = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Italic);

            Font reg = null;
            if (original.Bold)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold);
            else if (original.Underline)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Underline);
            else if (original.Strikeout)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Strikeout);
            else if (original.Italic && original.Underline)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Underline);
            else if (original.Italic && original.Strikeout)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Strikeout);
            else if (original.Underline && original.Strikeout)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Strikeout | FontStyle.Underline);
            else if (original.Italic && original.Strikeout && original.Underline)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Underline | FontStyle.Strikeout);
            else
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);

            if (GetCurrentDocument.SelectionFont.Italic) { GetCurrentDocument.SelectionFont = reg; }
            else { GetCurrentDocument.SelectionFont = italic; }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Font original = GetCurrentDocument.SelectionFont;
            Font under = null;
            if (original.Bold)
                under = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline | FontStyle.Bold);
            else if (original.Italic)
                under = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline | FontStyle.Italic);
            else if (original.Strikeout)
                under = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline | FontStyle.Strikeout);
            else if (original.Bold && original.Italic)
                under = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline | FontStyle.Bold | FontStyle.Underline);
            else if (original.Bold && original.Strikeout)
                under = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline | FontStyle.Bold | FontStyle.Strikeout);
            else if (original.Italic && original.Strikeout)
                under = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline | FontStyle.Strikeout | FontStyle.Italic);
            else if (original.Bold && original.Strikeout && original.Italic)
                under = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline | FontStyle.Bold | FontStyle.Italic | FontStyle.Strikeout);
            else
                under = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Underline);

            Font reg = null;
            if (original.Bold)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold);
            else if (original.Italic)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Italic);
            else if (original.Strikeout)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Strikeout);
            else if (original.Italic && original.Underline)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Underline);
            else if (original.Italic && original.Strikeout)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Strikeout);
            else if (original.Underline && original.Strikeout)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Strikeout | FontStyle.Underline);
            else if (original.Italic && original.Strikeout && original.Underline)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Underline | FontStyle.Strikeout);
            else
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
            
            if (GetCurrentDocument.SelectionFont.Underline) { GetCurrentDocument.SelectionFont = reg; }
            else { GetCurrentDocument.SelectionFont = under; }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Font original = GetCurrentDocument.SelectionFont;
            Font strike = null;
            if (original.Bold)
                strike = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout | FontStyle.Bold);
            else if (original.Italic)
                strike = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout | FontStyle.Italic);
            else if (original.Underline)
                strike = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout | FontStyle.Underline);
            else if (original.Bold && original.Italic)
                strike = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout | FontStyle.Bold | FontStyle.Italic);
            else if (original.Bold && original.Underline)
                strike = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout | FontStyle.Bold | FontStyle.Underline);
            else if (original.Italic && original.Underline)
                strike = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout | FontStyle.Italic | FontStyle.Italic);
            else if (original.Bold && original.Underline && original.Italic)
                strike = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout | FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            else
                strike = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Strikeout);

            Font reg = null;
            if (original.Bold)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold);
            else if (original.Italic)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Italic);
            else if (original.Underline)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Underline);
            else if (original.Bold && original.Underline)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Underline);
            else if (original.Italic && original.Underline)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Italic | FontStyle.Underline);
            else if (original.Bold && original.Italic)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Italic);
            else if (original.Italic && original.Strikeout && original.Italic)
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular | FontStyle.Bold | FontStyle.Underline | FontStyle.Italic);
            else
                reg = new Font(GetCurrentDocument.SelectionFont.FontFamily, GetCurrentDocument.SelectionFont.SizeInPoints, FontStyle.Regular);
            
            if (GetCurrentDocument.SelectionFont.Strikeout) { GetCurrentDocument.SelectionFont = reg; }
            else { GetCurrentDocument.SelectionFont = strike; }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectedText = GetCurrentDocument.SelectedText.ToUpper();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectedText = GetCurrentDocument.SelectedText.ToLower();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            float px = GetCurrentDocument.SelectionFont.SizeInPoints + 2;
            Font nf = new Font(GetCurrentDocument.SelectionFont.Name, px, GetCurrentDocument.SelectionFont.Style);

            GetCurrentDocument.SelectionFont = nf;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            float px = GetCurrentDocument.SelectionFont.SizeInPoints - 2;
            Font nf = new Font(GetCurrentDocument.SelectionFont.Name, px, GetCurrentDocument.SelectionFont.Style);

            GetCurrentDocument.SelectionFont = nf;
        }
        #endregion
        #region color
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                GetCurrentDocument.SelectionColor = colorDialog1.Color;
            }
        }

        private void HighlightGreen_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectionBackColor = Color.Lime;
        }

        private void HighlightOrange_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectionBackColor = Color.Orange;
        }

        private void HighlightYellow_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectionBackColor = Color.Yellow;
        }

        private void HighlightWhite_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectionBackColor = Color.White;
        }

        private void HighlightBlack_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectionBackColor = Color.Black;
        }
        #endregion
        #region font type
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GetCurrentDocument.SelectionFont != null)
            {
                String t = fontToolStripComboBox.ComboBox.GetItemText(fontToolStripComboBox.ComboBox.SelectedItem);

                Font nf = new Font(t, GetCurrentDocument.SelectionFont.Size, GetCurrentDocument.SelectionFont.Style);
                GetCurrentDocument.SelectionFont = nf;
            }
            else
            {
                String t = fontToolStripComboBox.ComboBox.GetItemText(fontToolStripComboBox.ComboBox.SelectedItem);
                GetCurrentDocument.SelectionFont = new Font(t, Int16.Parse(fontSizeToolStripComboBox.SelectedIndex.ToString()), FontStyle.Regular);
            }
            
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            float px;
            float.TryParse(fontSizeToolStripComboBox.SelectedItem.ToString(), out px);
            Font nf = new Font(GetCurrentDocument.SelectionFont.Name, px, GetCurrentDocument.SelectionFont.Style);

            GetCurrentDocument.SelectionFont = nf;
        }
        #endregion 

        private void Asys_Load(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Form Loading");
            if (Properties.Settings.Default.prefSaveLoc)
            {
                this.Location = Properties.Settings.Default.sysWinLoc;
                this.Size = Properties.Settings.Default.sysWinSize;
            }

            TabGenerator();
            GetFontCollection();
            SetFontSizes();

            openFileDialog1.RestoreDirectory = true;
            saveFileDialog1.RestoreDirectory = true;

            var args = System.Environment.GetCommandLineArgs();
            var argPath = args.Skip(1).FirstOrDefault();
            if (!string.IsNullOrEmpty(argPath))
            {
                var fullPath = Path.GetFullPath(argPath);

                if (fullPath.EndsWith(".rtf"))
                    GetCurrentDocument.LoadFile(fullPath, RichTextBoxStreamType.RichText);
                else
                    GetCurrentDocument.LoadFile(fullPath, RichTextBoxStreamType.PlainText);

                documentTab.SelectedTab.Text = Path.GetFileName(fullPath);
            }
            console.Append(GetTime() + "Form done loading");
        }

        private void TabGenerator()
        {
            if (Properties.Settings.Default.prefShowWelcome)
            {
                AddTab();
                string rtf = Properties.Resources.Welcome;
                GetCurrentDocument.Rtf = rtf;
                documentTab.SelectedTab.Text = "Welcome.rtf";
                documentTab.SelectedTab.Name = "asys_defaultWelcome.rtf";
                console.Append(GetTime() + "Displaying Welcome.rtf");
            }

            if (Properties.Settings.Default.prefShowChangelog)
            {
                AddTab();
                string rtf = Properties.Resources.Changelog;
                GetCurrentDocument.Rtf = rtf;
                documentTab.SelectedTab.Text = "Changelog.rtf";
                documentTab.SelectedTab.Name = "asys_defaultChangelog.rtf";
                console.Append(GetTime() + "Displaying Changelog.rtf");
            }
            
            if (!(Properties.Settings.Default.prefShowChangelog|Properties.Settings.Default.prefShowWelcome))
            {
                AddTab();
            }

            console.Append(GetTime() + "Checking for update");
            AsysExternalStringParser aesp = new AsysExternalStringParser();
            aesp.Load(a_prefs_url);
            string newver = aesp.ParseString("VERSION");
            if (!(newver == ""))
            {                
                ProcessVersion(newver);
            }
        }

        void ProcessVersion(string newver)
        {
            string[] curVersion = AsysAbout.VERSION.Split('.');
            string[] newVersion = newver.Split('.');

            int[] o = curVersion.Select(int.Parse).ToArray();
            int[] n = newVersion.Select(int.Parse).ToArray();

            bool update = false;
            for (int i = 0; i < o.Length; i++)
            {
                if (n[i] > o[i])
                {
                    console.Append(GetTime() + "Update Found! Showing update dialog");
                    update = true;
                    new AsysNewVerAvaliable(AsysAbout.VERSION, newver).ShowDialog();
                    break;
                }
            }
            if (!update)
            {
                console.Append(GetTime() + "No update found");
            }
        }

        #region menustrip
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTab();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing to open a file");
            if (GetCurrentDocument.Text != String.Empty) AddTab();
            string fileName = fileInteraction.open(GetCurrentDocument);

            documentTab.SelectedTab.Text = fileName;
            documentTab.SelectedTab.Name = fileName;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing to save a file");
            string fileName = Path.GetFileName(fileInteraction.save(GetCurrentDocument, documentTab.SelectedTab.Name));
            documentTab.SelectedTab.Text = fileName;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing to save a file");
            string fileName = Path.GetFileName(fileInteraction.saveAs(GetCurrentDocument, documentTab.SelectedTab.Name));
            documentTab.SelectedTab.Name = fileName;
            documentTab.SelectedTab.Text = fileName;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Closing");
            Application.Exit();
        }
        #endregion
        #region toolstrip
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            AddTab();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing to open a file");
            if (GetCurrentDocument.Text != String.Empty) AddTab();
            string fileName = fileInteraction.open(GetCurrentDocument);

            documentTab.SelectedTab.Text = fileName;
            documentTab.SelectedTab.Name = fileName;
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing to save a file");
            string fileName = Path.GetFileName(fileInteraction.save(GetCurrentDocument, documentTab.SelectedTab.Name));
            documentTab.SelectedTab.Text = fileName;
        }

        private void newToolStripButton_Click_1(object sender, EventArgs e)
        {
            AddTab();
        }

        private void openToolStripButton_Click_1(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing to open a file");
            if (GetCurrentDocument.Text != String.Empty) AddTab();
            string fileName = fileInteraction.open(GetCurrentDocument);

            documentTab.SelectedTab.Text = fileName;
        }

        private void saveToolStripButton_Click_1(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing to save a file");
            string fileName = Path.GetFileName(fileInteraction.save(GetCurrentDocument, documentTab.SelectedTab.Name));
            documentTab.SelectedTab.Text = fileName;
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            RemoveTab();
        }
        #endregion
        #region contextmenu
        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void redoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing to save a file");
            string fileName = Path.GetFileName(fileInteraction.save(GetCurrentDocument, documentTab.SelectedTab.Name));
            documentTab.SelectedTab.Text = fileName;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveTab();
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllTabs();
        }

        private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllTabsExceptThis();
        }
        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Launching About dialog");
            new AsysAbout().ShowDialog();
        }

        public static bool shouldClose = false;

        public static void SetShouldClose(bool flag)
        {
            shouldClose = flag;
        }

        private void Asys_FormClosing(object sender, FormClosingEventArgs e)
        {
            console.Append(GetTime() + "Close event invoked!");
            if (Properties.Settings.Default.prefSaveLoc)
            {
                console.Append(GetTime() + "Saving preferences");
                Properties.Settings.Default.sysWinLoc = this.Location;

                if (this.WindowState == FormWindowState.Normal)
                {
                    Properties.Settings.Default.sysWinSize = this.Size;
                }
                else
                {
                    Properties.Settings.Default.sysWinSize = this.RestoreBounds.Size;
                }
                Properties.Settings.Default.Save();
                console.Append(GetTime() + "Preferences saved!");
            }

            if (tabCount > 1)
            {
                if (!shouldClose)
                {
                    e.Cancel = true;
                    console.Append(GetTime() + "Canceling CloseEvent, showing dialog");
                    new AsysCloseHandler(tabCount, this).ShowDialog();
                }
                else 
                { 
                    e.Cancel = false;
                    console.Append(GetTime() + "Bye");
                }
            }
            else
            {
                if (!shouldClose)
                {
                    if (GetCurrentDocument.Text == String.Empty)
                    {
                        e.Cancel = false;
                        console.Append(GetTime() + "Empty document, closing");
                        shouldClose = true;
                        Application.Exit();
                    }
                    else
                    {
                        e.Cancel = true;
                        console.Append(GetTime() + "Canceling CloseEvent, showing dialog");
                        new AsysSingleTabCloseHandler().ShowDialog();
                    }                    
                }
                else
                {
                    e.Cancel = false;
                    console.Append(GetTime() + "Bye");
                }
            }
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing to load image");
            openFileDialog1.Filter = "All Files|*.*";
            openFileDialog1.Multiselect = true;
            var clipdata = Clipboard.GetDataObject();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string fname in openFileDialog1.FileNames)
                {
                    var img = Image.FromFile(fname);
                    Clipboard.SetImage(img);
                    GetCurrentDocument.Paste();
                }
            }
            Clipboard.SetDataObject(clipdata);
        }

        private void sidebarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Updating visability preferences");
            if (sidebarToolStripMenuItem.Checked)
            {
                toolStrip1.Visible = true;
                Properties.Settings.Default.prefShowSidebar = true;
            }
            else
            {
                toolStrip1.Visible = false;
                Properties.Settings.Default.prefShowSidebar = false;
            }
            Properties.Settings.Default.Save();
        }

        private void toolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Updating visability preferences");
            if (toolbarToolStripMenuItem.Checked)
            {
                toolStrip2.Visible = true;
                Properties.Settings.Default.prefShowToolbar = true;
            }
            else
            {
                toolStrip2.Visible = false;
                Properties.Settings.Default.prefShowToolbar = false;
            }
            Properties.Settings.Default.Save();
        }

        private void statusbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Updating visability preferences");
            if (statusbarToolStripMenuItem.Checked)
            {
                statusStrip1.Visible = true;
                Properties.Settings.Default.prefShowStatusbar = true;
            }
            else
            {
                statusStrip1.Visible = false;
                Properties.Settings.Default.prefShowStatusbar = false;
            }
            Properties.Settings.Default.prefShowSidebar = false;
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Updating visability preferences");
            new AsysHelpDialog().Show();
        }

        private void Asys_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            console.Append(GetTime() + "Launching Help dialog");
            new AsysHelpDialog().Show();
        }
        #endregion events

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectionAlignment = HorizontalAlignment.Left;
            rightAlignToolStripButton.Checked = false;
            centerAlignToolStripButton.Checked = false;
            leftAlignToolStripButton.Checked = true;
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectionAlignment = HorizontalAlignment.Center;
            rightAlignToolStripButton.Checked = false;
            centerAlignToolStripButton.Checked = true;
            leftAlignToolStripButton.Checked = false;
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectionAlignment = HorizontalAlignment.Right;
            rightAlignToolStripButton.Checked = true;
            centerAlignToolStripButton.Checked = false;
            leftAlignToolStripButton.Checked = false;
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (GetCurrentDocument.SelectionBullet)
            {
                GetCurrentDocument.SelectionBullet = false;
                bulletListToolStripButton.Checked = false;
            }
            else
            {
                GetCurrentDocument.SelectionBullet = true;
                bulletListToolStripButton.Checked = true;
            }
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing for input");
            string input = Microsoft.VisualBasic.Interaction.InputBox("Input Text:", "Input", "", -1, -1);
            if (input != "")
            {
                float pixels = GetCurrentDocument.SelectionFont.SizeInPoints;
                Font nf = new Font(GetCurrentDocument.SelectionFont.Name, pixels - 2, GetCurrentDocument.SelectionFont.Style);
                GetCurrentDocument.SelectionFont = nf;

                GetCurrentDocument.SelectionCharOffset = 10;
                GetCurrentDocument.SelectedText = input;
                GetCurrentDocument.SelectionCharOffset = 0;

                Font nf2 = new Font(GetCurrentDocument.SelectionFont.Name, pixels, GetCurrentDocument.SelectionFont.Style);
                GetCurrentDocument.SelectionFont = nf2;
            }
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing for input");
            string input = Microsoft.VisualBasic.Interaction.InputBox("Input Text:", "Input", "", -1, -1);
            if (input != "")
            {
                float pixels = GetCurrentDocument.SelectionFont.SizeInPoints;
                Font nf = new Font(GetCurrentDocument.SelectionFont.Name, pixels - 2, GetCurrentDocument.SelectionFont.Style);
                GetCurrentDocument.SelectionFont = nf;

                GetCurrentDocument.SelectionCharOffset = -10;
                GetCurrentDocument.SelectedText = input;
                GetCurrentDocument.SelectionCharOffset = 0;

                Font nf2 = new Font(GetCurrentDocument.SelectionFont.Name, pixels, GetCurrentDocument.SelectionFont.Style);
                GetCurrentDocument.SelectionFont = nf2;
            }
        }

        void body_SelectionChanged(object sender, EventArgs e)
        {
            /////////////////////
            // LINE MANAGEMENT //
            /////////////////////

            // Get the line.
            int index = GetCurrentDocument.SelectionStart;
            int line = GetCurrentDocument.GetLineFromCharIndex(index);

            toolStripStatusLabel7.Text = line + 1 + "";

            // Get the column.
            int firstChar = GetCurrentDocument.GetFirstCharIndexFromLine(line);
            int column = index - firstChar;

            toolStripStatusLabel5.Text = column + "";
            //////////////////////////
            // WORD/CHAR MANAGEMENT //
            //////////////////////////

            // Char count
            if (GetCurrentDocument.Text.Length > 0)
            {
                toolStripStatusLabel1.Text = GetCurrentDocument.Text.Length.ToString();
            }
            else
            {
                toolStripStatusLabel1.Text = "0";
            }
            // Word count
            char[] delims = new char[] { ' ', '\r', '\n' };
            toolStripStatusLabel3.Text = GetCurrentDocument.Text.Split(delims, StringSplitOptions.RemoveEmptyEntries).Length.ToString();

            /////////////////////
            // FONT MANAGEMENT //
            /////////////////////

            try
            {
                Font theFont = GetCurrentDocument.SelectionFont;

                int fontName = 0;
                foreach (var item in fontToolStripComboBox.Items)
                {
                    fontName++;
                    if (item.ToString().ToUpper().Equals(theFont.Name.ToUpper()))
                    {
                        fontToolStripComboBox.SelectedIndex = fontName - 1;
                        break;
                    }
                }

                fontSizeToolStripComboBox.SelectedIndex = (int)theFont.SizeInPoints;

                if (theFont.Bold)
                    boldToolStripButton.Checked = true;
                else
                    boldToolStripButton.Checked = false;
                if (theFont.Italic)
                    italicToolStripButton.Checked = true;
                else
                    italicToolStripButton.Checked = false;
                if (theFont.Underline)
                    underlineToolStripButton.Checked = true;
                else
                    underlineToolStripButton.Checked = false;
                if (theFont.Strikeout)
                    strikeoutToolStripButton.Checked = true;
                else
                    strikeoutToolStripButton.Checked = false;

                if (GetCurrentDocument.SelectionAlignment == HorizontalAlignment.Left)
                    leftAlignToolStripButton.Checked = true;
                else
                    leftAlignToolStripButton.Checked = false;
                if (GetCurrentDocument.SelectionAlignment == HorizontalAlignment.Center)
                    centerAlignToolStripButton.Checked = true;
                else
                    centerAlignToolStripButton.Checked = false;
                if (GetCurrentDocument.SelectionAlignment == HorizontalAlignment.Right)
                    rightAlignToolStripButton.Checked = true;
                else
                    rightAlignToolStripButton.Checked = false;

                if (GetCurrentDocument.SelectionBullet)
                    bulletListToolStripButton.Checked = true;
                else
                    bulletListToolStripButton.Checked = false;
            }
            catch (Exception) { ; }
        }

        private void newToolStripButton_Click_2(object sender, EventArgs e)
        {
            AddTab();
        }

        private void openToolStripButton_Click_2(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing to open a file");
            if (GetCurrentDocument.Text != String.Empty) AddTab();
            string fileName = fileInteraction.open(GetCurrentDocument);

            documentTab.SelectedTab.Text = fileName;
        }

        private void saveToolStripButton_Click_2(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing to save a file");
            string fileName = Path.GetFileName(fileInteraction.save(GetCurrentDocument, documentTab.SelectedTab.Name));
            documentTab.SelectedTab.Text = fileName;
        }

        private void cutToolStripButton_Click_1(object sender, EventArgs e)
        {
            Cut();
        }

        private void copyToolStripButton_Click_1(object sender, EventArgs e)
        {
            Copy();
        }

        private void pasteToolStripButton_Click_1(object sender, EventArgs e)
        {
            Paste();
        }

        private void toolStripButton10_Click_1(object sender, EventArgs e)
        {
            RemoveTab();
        }

        private void drawingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Loading Graphics Editor");
            new AsysGraphicEditor().ShowDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPreview();
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PageSetup();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Loading Preferences dialog");
            new AsysPrefs().ShowDialog();
        }

        private void Handeler_DragEnter(object sender, DragEventArgs e)
        {
            console.Append(GetTime() + "Preparing to recieve object from Drag");
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void Handeler_DragDrop(object sender, DragEventArgs e)
        {
            console.Append(GetTime() + "Loading object from Drag");
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in files)
            {
                if (GetCurrentDocument.Text != String.Empty) AddTab();

                documentTab.SelectedTab.Text = Path.GetFileName(fileInteraction.silentOpen(GetCurrentDocument, file));
            }
        }   
    
        public static string GetTime()
        {
            return "[" + DateTime.Now.ToString("h:mm:ss tt" + "] ");
        }

        private void openConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Opening Console");
            console.Show();
        }

        private void undoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetCurrentDocument.Undo();
        }

        private void redoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetCurrentDocument.Redo();
        }

        private void cutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetCurrentDocument.Cut();
        }

        private void copyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetCurrentDocument.Copy();
        }

        private void pasteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetCurrentDocument.Paste();
        }

        private void selectAllToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectAll();
        }
    }
}
