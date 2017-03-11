using System;

using System.Security.Principal;
using System.Diagnostics;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

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
                // Extract the contents of "pack.zip" to the 'Extras" folder
                ExtractPackZip(downPath + @"\pack.zip", Path.Combine(Path.GetDirectoryName(exePath), "extras"));
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

        /// <summary>
        /// Use the SharpZipLib library to extract the "Pack.Zip" file to the Asys installation directory
        /// </summary>
        /// <param name="zip"></param>
        /// <param name="outFolder"></param>
        public static void ExtractPackZip(string zip, string outFolder)
        {
            ZipFile zf = null;
            try {
                FileStream fs = File.OpenRead(zip);
                zf = new ZipFile(fs);

                foreach (ZipEntry zipEntry in zf) {
                    if (!zipEntry.IsFile) {
                        continue;   // Ignore directories
                    }
                    String entryFileName = zipEntry.Name;

                    byte[] buffer = new byte[4096];
                    Stream zipStream = zf.GetInputStream(zipEntry);

                    String fullZipToPath = Path.Combine(outFolder, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0) {
                        Directory.CreateDirectory(directoryName);
                    }

                    // Unzip the ile in buffered chunks
                    using (FileStream streamWriter = File.Create(fullZipToPath)) {
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
            }
            finally {
                if (zf != null) {
                    zf.IsStreamOwner = true; // Makes close() also shut underlying stream
                    zf.Close(); // Release resources
                }
            }
        }
        
        public string returnPath() { return Environment.CurrentDirectory; }
    }
}
