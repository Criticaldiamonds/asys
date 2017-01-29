using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.IO;

using RichTextBoxPrintCtrl;

namespace Asys
{
    class FileInteraction
    {
        SaveFileDialog saveDialog;
        OpenFileDialog openDialog;
        Dictionary<string, string> files;

        AsysConsole console;

        /// <summary>
        /// Initialize variables for use
        /// </summary>
        public void Init(AsysConsole console)
        {
            this.console = console;
            console.Append(Asys.GetTime() + "Initializating FileInteraction");

            saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Plain Text Files|*.txt|Rich Text Format|*.rtf|C# Files|*.cs|Java Source Files|*.java|All Files|*.*";
            saveDialog.Title = "Save";

            openDialog = new OpenFileDialog();
            openDialog.Filter = "Plain Text Files|*.txt|Rich Text Format|*.rtf|C# Files|*.cs|Java Source Files|*.java|All Files|*.*";
            openDialog.Title = "Open";

            files = new Dictionary<string, string>();
        }

        /// <summary>
        /// Open a file from the filesystem and adds it to the Map.
        /// </summary>
        /// <param name="rtbIn"></param>
        /// <returns>The filename returned by the OpenFileDialog</returns>
        public string open(RichTextBoxPrintCtrl.RichTextBoxPrintCtrl rtbIn)
        {
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                if (openDialog.FileName.Length > 0)
                {
                    string fileName = Path.GetFileName(openDialog.FileName);

                    if (fileName.EndsWith(".rtf"))
                    {
                        rtbIn.LoadFile(openDialog.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        rtbIn.LoadFile(openDialog.FileName, RichTextBoxStreamType.PlainText);
                    }
                    addFileToMap(openDialog.FileName, openDialog.FileName);
                    console.Append(Asys.GetTime() + "Opening file");
                    return fileName;
                }
                else { return ""; }
            }
            else { return ""; }
        }

        /// <summary>
        /// Opens a file without the use of a OpenFileDialog
        /// </summary>
        /// <param name="rtbIn"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string silentOpen(RichTextBoxPrintCtrl.RichTextBoxPrintCtrl rtbIn, string filePath)
        {
            //getCurrentDocument.AppendText(File.ReadAllText(filePath));
            if (filePath.EndsWith(".rtf"))
            {
                rtbIn.LoadFile(filePath, RichTextBoxStreamType.RichText);
            }
            else
            {
                rtbIn.LoadFile(filePath, RichTextBoxStreamType.PlainText);
            }
            addFileToMap(filePath, filePath);
            console.Append(Asys.GetTime() + "Opening file (SilentOpen)");
            return filePath;
        }
        
        /// <summary>
        /// Saves the current file to the system if it exists in the Map.
        /// If not, passes params to SaveAs()
        /// </summary>
        /// <param name="rtbIn"></param>
        /// <param name="fileNameIn"></param>
        public string save(RichTextBoxPrintCtrl.RichTextBoxPrintCtrl rtbIn, string fileNameIn)
        {
            string fileName = "";
            if (getFileFromMap(fileNameIn) != "")
            {
                // The file already exists in the Map so save it
                console.Append(Asys.GetTime() + "Instance exists in Map, saving");
                fileName = getFileFromMap(fileNameIn);
                if (fileName.EndsWith(".rtf"))
                {
                    rtbIn.SaveFile(fileName, RichTextBoxStreamType.RichText);
                }
                else
                {
                    rtbIn.SaveFile(fileName, RichTextBoxStreamType.PlainText);
                }
                console.Append(Asys.GetTime() + "Save Complete (SilentSave)");
                return fileName;
            }
            else
            {
                // The file does not exist in the Map so
                // Send it to SaveAs with the rtb and the initial fileName passed in
                console.Append(Asys.GetTime() + "Instance does not exist in Map! calling saveAs()");
                return saveAs(rtbIn, fileNameIn);
            }

        }

        /// <summary>
        /// Saves the current document with a save dialog.
        /// Adds the current document to the Map.
        /// </summary>
        /// <param name="rtbIn"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string saveAs(RichTextBoxPrintCtrl.RichTextBoxPrintCtrl rtbIn, string fileName)
        {
            saveDialog.FileName = fileName;
            saveDialog.Title = "Save As";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveDialog.FileName.Length > 0)
                {
                    if (saveDialog.FileName.EndsWith(".rtf"))
                    {
                        rtbIn.SaveFile(saveDialog.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        rtbIn.SaveFile(saveDialog.FileName, RichTextBoxStreamType.PlainText);
                    }
                    addFileToMap(fileName, saveDialog.FileName);
                    console.Append(Asys.GetTime() + "Save Complete");
                    return Path.GetFileName(saveDialog.FileName);
                }
                else { return ""; }
            }
            else { return ""; }
        }

        /// <summary>
        /// Adds a document to the Map for use in silent saving.
        /// </summary>
        /// <param name="rtbIn"></param>
        /// <param name="fileName"></param>
        public void addFileToMap(string keyName, string fileName)
        {
            try
            {
                console.Append(Asys.GetTime() + "Adding " + keyName + " to Map");
                files.Add(keyName, fileName);
            }
            catch (Exception)
            {
                // Assuming that it needs to be replaced due to 'Save As'
                console.Append(Asys.GetTime() + "[ERROR]: FileInteraction.addFileToMap: Instance already exists! Retrying...");
                RemoveFileFromMap(keyName);
                try
                {
                    console.Append(Asys.GetTime() + "Adding " + keyName + " to Map");
                    files.Add(keyName, fileName);
                }
                catch (Exception ex2)
                {
                    // If we get here there's clearly something wrong
                    console.Append(Asys.GetTime() + "[ERROR][FATAL]: FileInteraction.addFileToMap: Could not add to Map!");
                    MessageBox.Show("Error in FileInteraction.addFileToMap:\n" + ex2.Message);
                }
            }
        }

        /// <summary>
        /// Returns the filename associated with a given richtextbox.
        /// </summary>
        /// <param name="rtbIn"></param>
        /// <returns></returns>
        public string getFileFromMap(string keyIn)
        {
            string value = "";
            try
            {
                if (files.TryGetValue(keyIn, out value))
                {
                    return value;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                console.Append(Asys.GetTime() + "[ERROR]: FileInteraction.getFileFromMap: Could not find instance!");
                MessageBox.Show("Error in FileInteraction.getFileFromMap:\n" + ex.Message);
                return "";
            }
            
        }

        /// <summary>
        /// Removes the specified document from the Map
        /// </summary>
        /// <param name="keyIn"></param>
        public void RemoveFileFromMap(string keyIn)
        {
            try
            {
                console.Append(Asys.GetTime() + "Removing " + keyIn + " from Map");
                files.Remove(keyIn);
            }
            catch (Exception ex)
            {
                console.Append(Asys.GetTime() + "[ERROR]: FileInteraction.removeFileFromMap: Could not remove from Map!");
                MessageBox.Show("Error in FileInteraction.removeFileFromMap:\n" + ex.Message);
            }
        }

    }
}
