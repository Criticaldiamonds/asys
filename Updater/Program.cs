using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Principal;
using System.IO;
using System.Diagnostics;

namespace AsysExecutables
{
    public class AsysUpdate_
    {
        /// <summary>Gets the path for the executable that started the program, including the file name</summary>
        private static string _executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        /// <summary>Gets the path for the executable that started the program, not including the file name</summary>
        private static string _startupPath = System.IO.Path.GetDirectoryName(_executablePath);

        static void Main(string[] args)
        {
            bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator) ? true : false;

            if (args.Length == 0) Environment.Exit(0);
            string exePath  = args[0]; // Path to the Asys executable (Includes filename)
            string procName = args[1]; // Asys process name
            string downPath = args[2]; // Path to the Downloads directory

            // Remove the quotations
            exePath = exePath.Replace("\"", String.Empty).Trim();
            downPath = downPath.Replace("\"", String.Empty).Trim();

            if (isAdmin)
            {
                // Kill the Asys process if it exists
                Process.Start("taskkill", "/IM " + procName + ".exe /F");
                // Rename the Asys executable
                File.Move(exePath, exePath + ".bak");
                // Move the new executable to the Asys directory, renaming it in the process
                File.Move(downPath + @"\update.exe", exePath);
                // Delete the backup executable
                File.Delete(exePath + ".bak");
                //Start the new executable
                Process.Start(exePath);

                // Exit
                Environment.Exit(0);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Update failed: Updater must run with elevated privilages", "Asys Updater");
            }
        }

        public string returnPath() { return Environment.CurrentDirectory; }
    }
}
