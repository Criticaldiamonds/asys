using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Diagnostics;
using System.IO;
using System.Reflection;

using AsysEditor.Classes;

namespace AsysEditor.Forms
{
    public partial class AsysNewVerAvaliable : Form
    {
        string oldver, newver;
        
        public AsysNewVerAvaliable(string o, string n)
        {
            InitializeComponent();
            this.oldver = o;
            this.newver = n;
        }

        private void AsysNewVerAvaliable_Load(object sender, EventArgs e)
        {
            lblCurVer.Text = String.Format("Current Version: {0}", oldver);
            lblNewVer.Text = String.Format("New Version: {0}", newver);

            chkNever.Checked = Properties.Settings.Default.prefDisableAutoUpdate;
        }

        private void lnkDownloadNow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            update_new();
        }

        private void update_new()
        {
            // Download the new version

            btnWait.Enabled = false;
            lnkDownloadNow.Enabled = false;
            this.ControlBox = false;

            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_Completed);

                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                wc.DownloadFileAsync(new Uri("https://dl.dropboxusercontent.com/u/276558657/Asys/update.exe"), KnownFolders.GetPath(KnownFolder.Downloads) + @"\update.exe");
                
                progressBar1.Visible = true;
            }
        }
        
        private void update_old()
        {
            //string downloadspath = KnownFolders.GetPath(KnownFolder.Downloads);

            //btnWait.Enabled = false;
            //lnkDownloadNow.Enabled = false;
            //this.ControlBox = false;

            //using (WebClient wc = new WebClient())
            //{
            //    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
            //    wc.DownloadFileAsync(new System.Uri("https://github.com/Criticaldiamonds/asys/releases/download/" + newver + "/AsysInstaller.msi"),
            //        downloadspath + @"\AsysInstaller.msi");
            //    progressBar1.Visible = true;
            //}
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void wc_Completed(object sender, AsyncCompletedEventArgs e)
        {
            Run();
        }

        private void Run()
        {
            Process updaterProcess = new Process();
            updaterProcess.StartInfo.FileName = Application.StartupPath + @"\UpdateAsys_.exe";
            updaterProcess.StartInfo.Arguments = (char)34 + Application.ExecutablePath + (char)34 + " " +
                                                            Process.GetCurrentProcess().ProcessName + " " +
                                                 (char)34 + KnownFolders.GetPath(KnownFolder.Downloads) + (char)34;
            updaterProcess.Start();
                                                           

            //// Rename the Asys exe file
            //File.Move(Application.ExecutablePath, Application.ExecutablePath + ".bak");

            //// Create the batch file
            //StringBuilder batch = new StringBuilder();
            //batch.AppendLine("@echo off");
            //batch.AppendLine("taskkill /IM " + Process.GetCurrentProcess().ProcessName + ".exe /F");
            //batch.AppendLine("ping localhost > nul");
            //batch.AppendLine("ren " + (char)34 + KnownFolders.GetPath(KnownFolder.Downloads) + @"\update.exe" + (char)34 + " " + (char)34 + Application.ExecutablePath + (char)34); // (char)34 = quotation mark
            //batch.AppendLine((char)34 + Application.ExecutablePath + (char)34);
            //batch.AppendLine("del " + (char)34 + Application.ExecutablePath + ".bak" + (char)34);
            //batch.AppendLine("del %0");     // self-destruct

            //// Run the batch file
            //File.WriteAllText(Application.StartupPath + @"\update.bat", batch.ToString(), Encoding.Default);
            //Process.Start(Application.StartupPath + @"\update.bat");
        }

        void Install()
        {
            //if (!alreadyRunningInstaller)
            //{
            //    alreadyRunningInstaller = true;
            //    // Process.Start(KnownFolders.GetPath(KnownFolder.Downloads) + @"\AsysInstaller.msi");
            //    Asys.SetShouldClose(true);
            //    Asys.console.SetShouldClose();
                
            //    var assembly = Assembly.GetExecutingAssembly();

            //    using (Stream input = assembly.GetManifestResourceStream("AsysEditor.Supplementary_Files._install.bat"))
            //    using (Stream output = File.Create(KnownFolders.GetPath(KnownFolder.Downloads) + @"\____.bat"))
            //    {
            //        CopyStream(input, output);
            //    }

            //    var process = new Process
            //    {
            //        StartInfo =
            //        {
            //            Arguments = "\"" + KnownFolders.GetPath(KnownFolder.Downloads) + @"\AsysInstaller.msi" + "\""
            //        }
            //    };
            //    process.StartInfo.FileName = (KnownFolders.GetPath(KnownFolder.Downloads) + @"\____.bat");

            //    bool b = process.Start();

            //    // string[] assembelies = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();

            //    Application.Exit();
            //}
        }

        private void btnWait_Click(object sender, EventArgs e)
        {
            if (chkSkip.Checked)
            {
                Properties.Settings.Default.prefSkipUpdate = true;
                Properties.Settings.Default.sysSkippedVersion = newver;
            }
            if (chkNever.Checked)
            {
                Properties.Settings.Default.prefDisableAutoUpdate = true;
            }

            Properties.Settings.Default.Save();
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new AsysChangelogViewer("https://dl.dropboxusercontent.com/u/276558657/Asys/changelog.rtf").Show();
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8192];

            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }
    }
}
