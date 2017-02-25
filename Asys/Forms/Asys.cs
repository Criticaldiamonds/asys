using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;

using System.Reflection;

using AsysControls;

using AsysEditor.Classes;

namespace AsysEditor.Forms
{
    /// <summary>
    /// Representation of the Asys window.</summary>
    /// <remarks>
    /// The Asys form is the main source of interaction the end user has with the program.<br>
    /// The form contains the menu and toolstrips, along with the tab-control hosting the documents
    /// </remarks>
    public partial class Asys : Form
    {

        #region Variables

        /// <summary>Implementation of the FileInteraction class used by the form</summary>
        private FileInteraction fileInteraction;
        /// <summary>Implementation of the DocumentInteraction class used by the form</summary>
        private DocumentInteraction documentInteraction;
        /// <summary>Implementation of the Asys Updater system</summary>
        private AsysUpdater updater;
        /// <summary>Implementation of the console window</summary>
        public static AsysConsole console;

        /// <summary>Represents the number of tabs currently open</summary>
        private int tabCount = 0;
        /// <summary>Used as part of the printing system</summary>
        private int checkPrint;

        /// <summary>Used to determine whether the form is to close when form.Close() is called.<br>
        /// Set to true if "close" is selected in the CloseHandler or SingleTabClose Handler</summary>
        public static bool shouldClose = false;

        /// <summary>Gets the currently selected document in context</summary>
        /// <value>Returns the RichTextBoxPrintCtrl object currently in use</value>
        private RichTextBoxPrintCtrl GetCurrentDocument
        {
            get
            {
                return (RichTextBoxPrintCtrl)documentTab.SelectedTab.Controls["Body"];
            }
        }

        #endregion

        #region Startup

        /// <summary>Main entry point for the form</summary>
        public Asys()
        {
            Init();
        }

        /// <summary>Main entry print for the form when an associated file is opened by Windows Explorer</summary>
        /// <param name="filePath">The direct file path to the file being opened</param>
        public Asys(String filePath)
        {
            Init();
            AddTab();
            // Open the file and set the tab's text to the filename
            DocumentInfo info = fileInteraction.silentOpen(GetCurrentDocument, filePath, Int16.Parse(documentTab.SelectedTab.Name));

            documentTab.SelectedTab.Text = info.FileName;
            documentTab.SelectedTab.Name = info.ID + "";
        }

        /// <summary>Initializes crucial components and variables used by the Asys form</summary>
        private void Init()
        {
            InitializeComponent();

            console = new AsysConsole();
            console.Show();
            console.Hide();
            console.Append(GetTime() + "Asys Started");

            fileInteraction = new FileInteraction();
            fileInteraction.Init(console);
            documentInteraction = new DocumentInteraction();

            updater = new AsysUpdater();

            // Display toolars based on user preferences
            sidebarToolStripMenuItem.Checked = Properties.Settings.Default.prefShowSidebar;
            sidebarToolStrip.Visible = Properties.Settings.Default.prefShowSidebar;
            toolbarToolStripMenuItem.Checked = Properties.Settings.Default.prefShowToolbar;
            toolbarToolStrip.Visible = Properties.Settings.Default.prefShowToolbar;
            statusbarToolStripMenuItem.Checked = Properties.Settings.Default.prefShowStatusbar;
            statusBar.Visible = Properties.Settings.Default.prefShowStatusbar;
            console.Append(GetTime() + "Initialization complete");
        }

        /// <summary>Handles the initial form loading event</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Asys_Load(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Form Loading");

            // Get window preferences
            if (Properties.Settings.Default.prefSaveLoc)
            {
                this.Location = Properties.Settings.Default.sysWinLoc;
                this.Size = Properties.Settings.Default.sysWinSize;
            }

            // Delete previous installers to prevent confusion
            string searchPattern = "AsysInstaller*.*";
            DirectoryInfo dirInfo = new DirectoryInfo(KnownFolders.GetPath(KnownFolder.Downloads));
            FileInfo[] files = dirInfo.GetFiles(searchPattern);

                try
                {
                    foreach (FileInfo file in files) {
                        File.Delete(file.FullName);
                    }
                    File.Delete(KnownFolders.GetPath(KnownFolder.Downloads) + @"\AsysInstaller.msi");
                }
                catch (Exception)
                {
                    console.Append(GetTime() + "[ERROR]: Asys.Asys_Load: Could not delete previous installer!");
                }

            // Start the updater
            updater.Start(AsysAbout.AsysVersion);

            // Load stuff
            TabGenerator();
            GetFontCollection();
            SetFontSizes();

            // Check if there is a file waiting to be opened (via double-click)
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


        #endregion Startup

        #region Tabs

        /// <summary>Adds a new tab to the tab-control, along with necessary controls contained withen the tab</summary>
        public void AddTab()
        {
            // Create a new RichTextBox
            RichTextBoxPrintCtrl body = new RichTextBoxPrintCtrl();
            body.Name = "Body";
            body.Dock = DockStyle.Fill;
            body.ContextMenuStrip = contextMenuStrip1;
            body.AcceptsTab = true;
            body.SelectionChanged += body_SelectionChanged;

            // Create the tab
            TabPage t = new TabPage();
            tabCount += 1;

            String DocText = "Document " + tabCount;
            t.Text = DocText;
            t.Name = UCID.GenerateUCID() + "";
            t.Controls.Add(body);

            // Add the tab to the tab-control
            documentTab.TabPages.Add(t);
            documentTab.SelectedIndex = documentTab.TabPages.Count - 1;
            console.Append(GetTime() + "Tab successfully added");
        }

        /// <summary>Removes the current tab from the tab-control</summary>
        public void RemoveTab()
        {
            if (documentTab.TabPages.Count != 1)
            {
                // Remove the tab from the file map
                fileInteraction.RemoveFile(Int16.Parse(documentTab.SelectedTab.Name));
                documentTab.TabPages.Remove(documentTab.SelectedTab);
                tabCount -= 1;
                documentTab.SelectedIndex = documentTab.TabPages.Count - 1;
                console.Append(GetTime() + "Tab successfully removed");
            }
            else
            {
                // Remove the tab from the file map and create a new tab
                fileInteraction.RemoveFile(Int16.Parse(documentTab.SelectedTab.Name));
                documentTab.TabPages.Remove(documentTab.SelectedTab);
                tabCount -= 1;
                console.Append(GetTime() + "Tab successfully removed");
                AddTab();
            }
        }

        /// <summary>Removes all tabs from the tab-control</summary>
        private void RemoveAllTabs()
        {
            foreach (TabPage p in documentTab.TabPages)
            {
                documentTab.TabPages.Remove(p);
                tabCount -= 1;
            }
            AddTab();
        }

        /// <summary>Removes all tabs, excluding the currently selected tab, from the tab-control</summary>
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

        /// <summary>Determines which tabs need to be created on startup.<br>
        /// (Blank, Welcome, Changelog, etc)</summary>
        private void TabGenerator()
        {
            // Check whether to show the Welcome file
            if (Properties.Settings.Default.prefShowWelcome)
            {
                AddTab();
                string rtf = Properties.Resources.Welcome;
                GetCurrentDocument.Rtf = rtf;
                documentTab.SelectedTab.Text = "Welcome.rtf";
                documentTab.SelectedTab.Name = "asysdefault_Welcome.rtf";
                console.Append(GetTime() + "Displaying Welcome.rtf");
            }

            // Check whether to show the Changelog file
            if (Properties.Settings.Default.prefShowChangelog)
            {
                AddTab();
                string rtf = Properties.Resources.Changelog;
                GetCurrentDocument.Rtf = rtf;
                documentTab.SelectedTab.Text = "Changelog.rtf";
                documentTab.SelectedTab.Name = "asysdefault_Changelog.rtf";
                console.Append(GetTime() + "Displaying Changelog.rtf");
            }

            // Just show a blank tab
            if (!(Properties.Settings.Default.prefShowChangelog | Properties.Settings.Default.prefShowWelcome))
            {
                AddTab();
            }
        }

        #endregion Tabs

        #region Printing

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

        #region Printing Button Events

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

        #endregion Printing Button Events

        #endregion Printing

        #region Font Management

        /// <summary>Gets a list of all fonts currently installed on the user's system</summary>
        private void GetFontCollection()
        {
            try
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
            catch (Exception ex)
            {
                console.Append(GetTime() + "[ERROR]: Asys.GetFontCollection: " + ex.Message);
            }
        }

        /// <summary>Generates a list of font sizes from 1 - 75</summary>
        private void SetFontSizes()
        {
            try
            {
                for (int i = 0; i < 75; i++)
                {
                    fontSizeToolStripComboBox.Items.Add(i);
                }
                fontSizeToolStripComboBox.SelectedIndex = 12; // 12pt
            }
            catch (Exception ex)
            {
                console.Append(GetTime() + "[ERROR]: Asys.SetFontSizes: " + ex.Message);
            }
        }

        /// <summary>Changes the font of the current document to the one selected by the user</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>Changes the font size of the current document to the one selected by the user</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            float px;
            float.TryParse(fontSizeToolStripComboBox.SelectedItem.ToString(), out px);
            Font nf = new Font(GetCurrentDocument.SelectionFont.Name, px, GetCurrentDocument.SelectionFont.Style);

            GetCurrentDocument.SelectionFont = nf;
        }

        #endregion Font Management

        #region Generic Button Events

        /// <summary>Handles the creation of tabs for all 'New' buttons</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void new_ButtonClickEvent(object sender, EventArgs e)
        {
            AddTab();
        }

        /// <summary>Handles the 'Cut' event for all applicable buttons</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cut_ButtonClickEvent(object sender, EventArgs e)
        {
            documentInteraction.Cut(GetCurrentDocument);
        }

        /// <summary>Handles the 'Copy' event for all applicable buttons</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copy_ButtonClickEvent(object sender, EventArgs e)
        {
            documentInteraction.Copy(GetCurrentDocument);
        }

        /// <summary>Handles the 'Paste' event for all applicable buttons</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void paste_ButtonClickEvent(object sender, EventArgs e)
        {
            documentInteraction.Paste(GetCurrentDocument);
        }

        /// <summary>Handles the 'Undo' event for all applicable buttons</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void undo_ButtonClickEvent(object sender, EventArgs e)
        {
            documentInteraction.Undo(GetCurrentDocument);
        }

        /// <summary>Handles the 'Redo' event for all applicable buttons</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void redo_ButtonClickEvent(object sender, EventArgs e)
        {
            documentInteraction.Redo(GetCurrentDocument);
        }

        /// <summary>Handles the 'Select All' event for all applicable buttons</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAll_ButtonClickEvent(object sender, EventArgs e)
        {
            documentInteraction.SelectAll(GetCurrentDocument);
        }

        #endregion

        #region Text Modifier Button Events

        // Bold
        private void boldToolStripButton_Click(object sender, EventArgs e)
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

        // Italic
        private void italicToolStripButton_Click(object sender, EventArgs e)
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

        // Underline
        private void underlineToolStripButton_Click(object sender, EventArgs e)
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

        // Strikeout
        private void strikeoutToolStripButton_Click(object sender, EventArgs e)
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

        // UPPERCASE
        private void uppercaseToolStripButton_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectedText = GetCurrentDocument.SelectedText.ToUpper();
        }

        // lowercase
        private void lowercaseToolStripButton_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectedText = GetCurrentDocument.SelectedText.ToLower();
        }

        // Size Up
        private void sizeUpToolStripButton_Click(object sender, EventArgs e)
        {
            float px = GetCurrentDocument.SelectionFont.SizeInPoints + 2;
            Font nf = new Font(GetCurrentDocument.SelectionFont.Name, px, GetCurrentDocument.SelectionFont.Style);

            GetCurrentDocument.SelectionFont = nf;
        }

        // Size Down
        private void sizeDownToolStripButton_Click(object sender, EventArgs e)
        {
            float px = GetCurrentDocument.SelectionFont.SizeInPoints - 2;
            Font nf = new Font(GetCurrentDocument.SelectionFont.Name, px, GetCurrentDocument.SelectionFont.Style);

            GetCurrentDocument.SelectionFont = nf;
        }

        // Text Color
        private void forecolorToolStripButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                GetCurrentDocument.SelectionColor = colorDialog1.Color;
            }
        }

        #region Highlighting

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

        #endregion Highlighting

        #endregion Text Modifier Button Events

        #region Filesystem Button Events

        /// <summary>Called when the 'Open' menu item is pressed</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing to open a file");

            if (tabCount > 1) AddTab();

            DocumentInfo info = fileInteraction.open(GetCurrentDocument, Int16.Parse(documentTab.SelectedTab.Name));

            // Set the tab name and text to the filename
            documentTab.SelectedTab.Text = info.FileName;
            documentTab.SelectedTab.Name = info.ID + ""; 
        }

        /// <summary>Called when the 'Save' menu item is pressed</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the file is NOT the Changelog or Welcome files
            if (!(documentTab.SelectedTab.Name.StartsWith("asysdefault_")))
            {
                console.Append(GetTime() + "Preparing to save a file");

                DocumentInfo info = fileInteraction.save(GetCurrentDocument, Int16.Parse(documentTab.SelectedTab.Name));

                // Set the tab text to the filename
                documentTab.SelectedTab.Text = info.FileName;
                documentTab.SelectedTab.Name = info.ID +"";
            }
        }

        /// <summary>Called when the 'Save As' menu item is pressed</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing to save a file");

            DocumentInfo info = fileInteraction.saveAs(GetCurrentDocument, documentTab.SelectedTab.Text, Int16.Parse(documentTab.SelectedTab.Name));
            
            // Set the tab name and text to the filename
            documentTab.SelectedTab.Text = info.FileName;
            documentTab.SelectedTab.Name = info.ID + "";
        }

        // Context Menu
        /// <summary>Called when the 'Save' menu item is pressed</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // If the file is NOT the Changelog or Welcome files
            if (!(documentTab.SelectedTab.Name.StartsWith("asysdefault_")))
            {
                console.Append(GetTime() + "Preparing to save a file");

                DocumentInfo info = fileInteraction.save(GetCurrentDocument, Int16.Parse(documentTab.SelectedTab.Name));

                // Set the tab text to the filename
                documentTab.SelectedTab.Text = info.FileName;
                documentTab.SelectedTab.Name = info.ID + "";
            }
        }

        // Sidebar
        /// <summary>Called when the 'Open' menu item is pressed</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing to open a file");

            if (tabCount > 1) AddTab();

            DocumentInfo info = fileInteraction.open(GetCurrentDocument, Int16.Parse(documentTab.SelectedTab.Name));

            // Set the tab name and text to the filename
            documentTab.SelectedTab.Text = info.FileName;
            documentTab.SelectedTab.Name = info.ID + ""; 
        }

        // Sidebar
        /// <summary>Called when the 'Open' menu item is pressed</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            // If the file is NOT the Changelog or Welcome files
            if (!(documentTab.SelectedTab.Name.StartsWith("asysdefault_")))
            {
                console.Append(GetTime() + "Preparing to save a file");

                DocumentInfo info = fileInteraction.save(GetCurrentDocument, Int16.Parse(documentTab.SelectedTab.Name));

                // Set the tab text to the filename
                documentTab.SelectedTab.Text = info.FileName;
                documentTab.SelectedTab.Name = info.ID + "";
            }
        }

        #endregion Filesystem Button Events

        #region Drag and Drop Events

        /// <summary>Called when the user attemps to drag and drop a file into the form</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Handeler_DragEnter(object sender, DragEventArgs e)
        {
            console.Append(GetTime() + "Preparing to recieve object from Drag");
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        /// <summary>Called when the user completes the drag and drop process</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Handeler_DragDrop(object sender, DragEventArgs e)
        {
            console.Append(GetTime() + "Loading object from Drag");
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in files)
            {
                if (GetCurrentDocument.Text != String.Empty) AddTab();

                DocumentInfo info = fileInteraction.silentOpen(GetCurrentDocument, file, Int16.Parse(documentTab.SelectedTab.Name));

                documentTab.SelectedTab.Text = info.FileName;
                documentTab.SelectedTab.Name = info.ID + "";
            }
        }

        #endregion Drag and Frop Events

        #region GUI Visibility Button Events

        /// <summary>Toggles the visibility of the Sidebar, and updates the preferences to reflect on this action</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sidebarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Updating visability preferences");
            if (sidebarToolStripMenuItem.Checked)
            {
                sidebarToolStrip.Visible = true;
                Properties.Settings.Default.prefShowSidebar = true;
            }
            else
            {
                sidebarToolStrip.Visible = false;
                Properties.Settings.Default.prefShowSidebar = false;
            }
            Properties.Settings.Default.Save();
        }

        /// <summary>Toggles the visibility of the Toolbar, and updates the preferences to reflect on this action</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Updating visability preferences");
            if (toolbarToolStripMenuItem.Checked)
            {
                toolbarToolStrip.Visible = true;
                Properties.Settings.Default.prefShowToolbar = true;
            }
            else
            {
                toolbarToolStrip.Visible = false;
                Properties.Settings.Default.prefShowToolbar = false;
            }
            Properties.Settings.Default.Save();
        }

        /// <summary>Toggles the visibility of the Status bar, and updates the preferences to reflect on this action</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statusbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Updating visability preferences");
            if (statusbarToolStripMenuItem.Checked)
            {
                statusBar.Visible = true;
                Properties.Settings.Default.prefShowStatusbar = true;
            }
            else
            {
                statusBar.Visible = false;
                Properties.Settings.Default.prefShowStatusbar = false;
            }
            Properties.Settings.Default.prefShowSidebar = false;
        }

        #endregion GUI Visibility Button Events

        #region Insert Button Events

        /// <summary>Called when the user clicks on the 'Insert -> Image' button</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Preparing to load image");
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "All Files|*.*";
            openDialog.Multiselect = true;
            // Get the data currently in the Clipboard
            var clipdata = Clipboard.GetDataObject();

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string fname in openDialog.FileNames)
                {
                    var img = Image.FromFile(fname);
                    Clipboard.SetImage(img);
                    GetCurrentDocument.Paste();
                }
            }
            // Set the Clipboard back to its initial state
            Clipboard.SetDataObject(clipdata);
        }

        #endregion Insert Button Events

        #region Extra Events

        #region Buttons
        /// <summary>Called when the user clicks the 'About' button</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Launching About dialog");
            new AsysAbout().ShowDialog();
        }

        /// <summary>Called when the user clicks the 'Help' button</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Updating visability preferences");
            new AsysHelpDialog().Show();
        }

        /// <summary>Called when the user clicks the 'Preferences' button</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Loading Preferences dialog");
            new AsysPrefs().ShowDialog();
        }

        /// <summary>Called when the user clicks the 'Open Console' button (Hidden, but uses CTRL+M shortcut)</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Opening Console");
            console.Show();
        }

        #endregion Buttons

        // TODO: NOT WORKING
        private void documentTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!documentTab.SelectedTab.Name.StartsWith(""))
            //{
            //    int id = Int16.Parse(documentTab.SelectedTab.Name);

            //    switch (fileInteraction.GetFileType(id))
            //    {
            //        case EFileType.RICH_TEXT:
            //            {
            //                // Enable text formatting tools
            //                boldToolStripButton.Enabled = true;
            //                italicToolStripButton.Enabled = true;
            //                underlineToolStripButton.Enabled = true;
            //                strikeoutToolStripButton.Enabled = true;
            //                sizeUpToolStripButton.Enabled = true;
            //                sizeDownToolStripButton.Enabled = true;
            //                textColorToolStripButton.Enabled = true;
            //                highlightColorToolStripDropDownButton.Enabled = true;
            //                leftAlignToolStripButton.Enabled = true;
            //                centerAlignToolStripButton.Enabled = true;
            //                rightAlignToolStripButton.Enabled = true;
            //                bulletListToolStripButton.Enabled = true;
            //                superscriptToolStripButton.Enabled = true;
            //                subscriptToolStripButton.Enabled = true;
            //                fontToolStripComboBox.Enabled = true;
            //                fontSizeToolStripComboBox.Enabled = true;

            //                break;
            //            }
            //        case EFileType.PLAIN_TEXT:
            //            {
            //                // Disable text formatting tools
            //                boldToolStripButton.Enabled = false;
            //                italicToolStripButton.Enabled = false;
            //                underlineToolStripButton.Enabled = false;
            //                strikeoutToolStripButton.Enabled = false;
            //                sizeUpToolStripButton.Enabled = false;
            //                sizeDownToolStripButton.Enabled = false;
            //                textColorToolStripButton.Enabled = false;
            //                highlightColorToolStripDropDownButton.Enabled = false;
            //                leftAlignToolStripButton.Enabled = false;
            //                centerAlignToolStripButton.Enabled = false;
            //                rightAlignToolStripButton.Enabled = false;
            //                bulletListToolStripButton.Enabled = false;
            //                superscriptToolStripButton.Enabled = false;
            //                subscriptToolStripButton.Enabled = false;
            //                fontToolStripComboBox.Enabled = false;
            //                fontSizeToolStripComboBox.Enabled = false;

            //                break;
            //            }
            //        case EFileType.OTHER:
            //            {
            //                // Enable text formatting tools
            //                boldToolStripButton.Enabled = true;
            //                italicToolStripButton.Enabled = true;
            //                underlineToolStripButton.Enabled = true;
            //                strikeoutToolStripButton.Enabled = true;
            //                sizeUpToolStripButton.Enabled = true;
            //                sizeDownToolStripButton.Enabled = true;
            //                textColorToolStripButton.Enabled = true;
            //                highlightColorToolStripDropDownButton.Enabled = true;
            //                leftAlignToolStripButton.Enabled = true;
            //                centerAlignToolStripButton.Enabled = true;
            //                rightAlignToolStripButton.Enabled = true;
            //                bulletListToolStripButton.Enabled = true;
            //                superscriptToolStripButton.Enabled = true;
            //                subscriptToolStripButton.Enabled = true;
            //                fontToolStripComboBox.Enabled = true;
            //                fontSizeToolStripComboBox.Enabled = true;

            //                break;
            //            }
            //        default:
            //            {
            //                // Enable text formatting tools
            //                boldToolStripButton.Enabled = true;
            //                italicToolStripButton.Enabled = true;
            //                underlineToolStripButton.Enabled = true;
            //                strikeoutToolStripButton.Enabled = true;
            //                sizeUpToolStripButton.Enabled = true;
            //                sizeDownToolStripButton.Enabled = true;
            //                textColorToolStripButton.Enabled = true;
            //                highlightColorToolStripDropDownButton.Enabled = true;
            //                leftAlignToolStripButton.Enabled = true;
            //                centerAlignToolStripButton.Enabled = true;
            //                rightAlignToolStripButton.Enabled = true;
            //                bulletListToolStripButton.Enabled = true;
            //                superscriptToolStripButton.Enabled = true;
            //                subscriptToolStripButton.Enabled = true;
            //                fontToolStripComboBox.Enabled = true;
            //                fontSizeToolStripComboBox.Enabled = true;

            //                break;
            //            }
            //    }
            //}
        }

        private void Asys_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            console.Append(GetTime() + "Launching Help dialog");
            new AsysHelpDialog().Show();
        }

        // Update text properties, etc.
        /// <summary>Called when the text in the document changes</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                // Update styling buttons

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

        /// <summary>
        /// Gets the current time
        /// </summary>
        /// <returns>The current time in h:mm:ss tt format</returns>
        public static string GetTime()
        {
            return "[" + DateTime.Now.ToString("h:mm:ss tt" + "] ");
        }

        #endregion Extra (Button) Events

        #region Text Alignment & List Button Events

        private void leftAlignToolStripButton_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectionAlignment = HorizontalAlignment.Left;
            rightAlignToolStripButton.Checked = false;
            centerAlignToolStripButton.Checked = false;
            leftAlignToolStripButton.Checked = true;
        }

        private void centerAlignToolStripButton_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectionAlignment = HorizontalAlignment.Center;
            rightAlignToolStripButton.Checked = false;
            centerAlignToolStripButton.Checked = true;
            leftAlignToolStripButton.Checked = false;
        }

        private void rightAlignToolStripButton_Click(object sender, EventArgs e)
        {
            GetCurrentDocument.SelectionAlignment = HorizontalAlignment.Right;
            rightAlignToolStripButton.Checked = true;
            centerAlignToolStripButton.Checked = false;
            leftAlignToolStripButton.Checked = false;
        }

        private void bulletListToolStripButton_Click(object sender, EventArgs e)
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
        #endregion Text Alignment & List Button Events

        #region Super/Subscript Button Events

        private void superscriptToolStripButton_Click(object sender, EventArgs e)
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

        private void subscriptToolStripButton_Click(object sender, EventArgs e)
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

        #endregion Super/Subscript Button Events

        #region Tab Close Events

        private void close_ButtonClickEvent(object sender, EventArgs e)
        {
            RemoveTab();
        }

        private void closeAll_ButtonClickEvent(object sender, EventArgs e)
        {
            RemoveAllTabs();
        }

        private void closeAllButThis_ButtonClickEvent(object sender, EventArgs e)
        {
            RemoveAllTabsExceptThis();
        }

        #endregion

        #region Shutdown

        /// <summary>Sets the ShouldClose flag</summary>
        /// <param name="flag"></param>
        public static void SetShouldClose(bool flag)
        {
            shouldClose = flag;
        }

        /// <summary>
        /// Called when the 'Exit' button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Append(GetTime() + "Closing");
            this.Close();
            Application.Exit();
        }

        /// <summary>
        /// When the form is closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Asys_FormClosing(object sender, FormClosingEventArgs e)
        {
            console.Append(GetTime() + "Close event invoked!");
            if (Properties.Settings.Default.prefSaveLoc)
            {
                console.Append(GetTime() + "Saving preferences");

                // Window location
                Properties.Settings.Default.sysWinLoc = this.Location;
                                // Window size
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
                    // Display the Close-Handler if there is more than one tab open, and ShouldClose == FALSE
                    e.Cancel = true;
                    console.Append(GetTime() + "Canceling CloseEvent, showing dialog");
                    new AsysCloseHandler(tabCount, this).ShowDialog();
                }
                else
                {
                    // ShouldClose == TRUE; continue closing
                    e.Cancel = false;
                    console.Append(GetTime() + "Bye");
                }
            }
            else
            {
                // Only one tab
                if (!shouldClose)
                {
                    // Sisplay the Single tab close-handler if ShouldClose == FALSE

                    // If there is nothing in the document, close
                    if (GetCurrentDocument.Text == String.Empty)
                    {
                        e.Cancel = false;
                        console.Append(GetTime() + "Empty document, closing");
                        shouldClose = true;
                        Application.Exit();
                    }
                    else
                    {
                        // There is something in the document, ask for conformation
                        e.Cancel = true;
                        console.Append(GetTime() + "Canceling CloseEvent, showing dialog");
                        new AsysSingleTabCloseHandler().ShowDialog();
                    }
                }
                else
                {
                    // ShouldClose == TRUE; continue closing
                    e.Cancel = false;
                    console.Append(GetTime() + "Bye");
                }
            }
        }

        #endregion Shutdown
        
    }
}
