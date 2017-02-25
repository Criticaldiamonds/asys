using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.IO;

using RichTextBoxPrintCtrl;

using AsysEditor.Forms;

namespace AsysEditor.Classes
{
    class FileInteraction
    {
        SaveFileDialog saveDialog;
        OpenFileDialog openDialog;

        // All file paths for open documents [UCID, FilePath]
        Dictionary<int, string>      fileNames;
        // All file types for open documents [UCID, File Type]
        Dictionary<int, EFileType> fileTypes; // TODO: How to do this with new DocumentInfo class??

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

            fileNames = new Dictionary<int, string>();
            fileTypes = new Dictionary<int, EFileType>();
        }

        /// <summary>
        /// Open a file from the filesystem and adds it to the Map.
        /// </summary>
        /// <param name="rtbIn"></param>
        /// <param name="ucid"></param>
        /// <returns>The filename returned by the OpenFileDialog</returns>
        public DocumentInfo open(RichTextBoxPrintCtrl.RichTextBoxPrintCtrl rtbIn, int ucid)
        {
            DocumentInfo result = new DocumentInfo();
            result.ID = ucid;

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                if (openDialog.FileName.Length > 0)
                {
                    string fileName = openDialog.FileName;

                    if (fileName.EndsWith(".rtf"))
                    {
                        rtbIn.LoadFile(fileName, RichTextBoxStreamType.RichText);
                        result.FileType = EFileType.RICH_TEXT;
                    }
                    else
                    {
                        rtbIn.LoadFile(fileName, RichTextBoxStreamType.PlainText);

                        // Set file type
                        if (fileName.EndsWith(".txt"))
                            result.FileType = EFileType.PLAIN_TEXT;
                        else
                            result.FileType = EFileType.OTHER;
                    }

                    // Add file information to dictionaries
                    AddFile(ucid, fileName, result.FileType);

                    result.FileName = Path.GetFileName(fileName);
                    console.Append(Asys.GetTime() + "Opening file");
                    
                    return result;
                }
                else { return result; }
            }
            else { return result; }
        }

        /// <summary>
        /// Opens a file without the use of a OpenFileDialog
        /// </summary>
        /// <param name="rtbIn"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DocumentInfo silentOpen(RichTextBoxPrintCtrl.RichTextBoxPrintCtrl rtbIn, string filePath, int ucid)
        {
            DocumentInfo result = new DocumentInfo();
            result.ID = ucid;
            result.FileName = Path.GetFileName(filePath);

            //getCurrentDocument.AppendText(File.ReadAllText(filePath));
            if (filePath.EndsWith(".rtf"))
            {
                rtbIn.LoadFile(filePath, RichTextBoxStreamType.RichText);
                result.FileType = EFileType.RICH_TEXT;
            }
            else
            {
                rtbIn.LoadFile(filePath, RichTextBoxStreamType.PlainText);

                // Set file type
                if (filePath.EndsWith(".txt"))
                    result.FileType = EFileType.PLAIN_TEXT;
                else
                    result.FileType = EFileType.OTHER;
            }

            // Add file information to dictionaries
            AddFile(ucid, filePath, result.FileType);
            console.Append(Asys.GetTime() + "Opening file (SilentOpen)");

            return result;
        }
        
        /// <summary>
        /// Saves the current file to the system if it exists in the Map.
        /// If not, passes params to SaveAs()
        /// </summary>
        /// <param name="rtbIn"></param>
        /// <param name="ucid">Unique identifier of the document</param>
        public DocumentInfo save(RichTextBoxPrintCtrl.RichTextBoxPrintCtrl rtbIn, int ucid)
        {
            DocumentInfo result = new DocumentInfo();
            result.ID = ucid;

            string fileName = "";

            if (GetFilePath(ucid) != "")
            {
                // The file already exists in the Map so save it
                console.Append(Asys.GetTime() + "Instance exists in Map, saving");
                fileName = GetFilePath(ucid);

                if (fileName.EndsWith(".rtf"))
                {
                    rtbIn.SaveFile(fileName, RichTextBoxStreamType.RichText);
                    result.FileType = EFileType.RICH_TEXT;
                }
                else
                {
                    rtbIn.SaveFile(fileName, RichTextBoxStreamType.PlainText);

                    // Set file type
                    if (fileName.EndsWith(".txt"))
                        result.FileType = EFileType.PLAIN_TEXT;
                    else
                        result.FileType = EFileType.OTHER;
                }

                console.Append(Asys.GetTime() + "Save Complete (SilentSave)");

                result.FileName = Path.GetFileName(fileName);

                return result;
            }
            else
            {
                // The file does not exist in the Map so
                // Send it to SaveAs with the rtb and the initial fileName passed in
                console.Append(Asys.GetTime() + "Instance does not exist in Map! calling saveAs()");

                return saveAs(rtbIn, fileName, ucid);
            }

        }

        /// <summary>
        /// Saves the current document with a save dialog.
        /// Adds the current document to the Map.
        /// </summary>
        /// <param name="rtbIn"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public DocumentInfo saveAs(RichTextBoxPrintCtrl.RichTextBoxPrintCtrl rtbIn, string _fileName, int ucid)
        {
            DocumentInfo result = new DocumentInfo();
            result.ID = ucid;

            saveDialog.FileName = _fileName;
            saveDialog.Title = "Save As";

            string newName = String.Empty;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveDialog.FileName.Length > 0)
                {
                    newName = saveDialog.FileName;
                    result.FileName = Path.GetFileName(newName);

                    if (newName.EndsWith(".rtf"))
                    {
                        rtbIn.SaveFile(newName, RichTextBoxStreamType.RichText);
                        result.FileType = EFileType.RICH_TEXT;
                    }
                    else
                    {
                        rtbIn.SaveFile(newName, RichTextBoxStreamType.PlainText);

                        // Set file type
                        if (newName.EndsWith(".txt"))
                            result.FileType = EFileType.PLAIN_TEXT;
                        else
                            result.FileType = EFileType.OTHER;
                    }

                    // Add file information to dictionaries
                    AddFile(ucid, newName, result.FileType);

                    console.Append(Asys.GetTime() + "Save Complete");

                    return result;
                }
                else { return result; }
            }
            else { return result; }
        }

        /// <summary>
        /// Adds a filepath and FileType to the Maps for use in silent saving.
        /// </summary>
        /// <param name="UCID">The UCID associated with the document</param>
        /// <param name="fileName">The path of the file</param>
        /// <param name="fileType">The type of file</param>
        public void AddFile(int UCID, string fileName, EFileType fileType)
        {
            try
            {
                console.Append(Asys.GetTime() + "Adding " + UCID + " to Maps");
                fileNames.Add(UCID, fileName);
                fileTypes.Add(UCID, fileType);
            }
            catch (Exception)
            {
                // Assuming that it needs to be replaced due to 'Save As'
                console.Append(Asys.GetTime() + "[ERROR]: FileInteraction.AddFilePath: Instance already exists! Retrying...");
                RemoveFile(UCID);
                try
                {
                    console.Append(Asys.GetTime() + "Adding " + UCID + " to Map");
                    fileNames.Add(UCID, fileName);
                }
                catch (Exception ex2)
                {
                    // If we get here there's clearly something wrong
                    console.Append(Asys.GetTime() + "[ERROR][FATAL]: FileInteraction.AddFilePath: Could not add to Map!");
                    MessageBox.Show("Error in FileInteraction.AddFilePath:\n" + ex2.Message);
                }
            }
        }

        /// <summary>Removes the specified document from the Maps</summary>
        /// <param name="ucid">The UCID associated with the document</param>
        public void RemoveFile(int ucid)
        {
            try
            {
                console.Append(Asys.GetTime() + "Removing " + ucid + " from Map");
                fileNames.Remove(ucid);
                fileTypes.Remove(ucid);

                UCID.RemoveUCID(ucid);
            }
            catch (Exception ex)
            {
                console.Append(Asys.GetTime() + "[ERROR]: FileInteraction.RemoveFile: Could not remove from Maps!");
                MessageBox.Show("Error in FileInteraction.RemoveFile:\n" + ex.Message);
            }
        }

        /// <summary>Returns the file-path associated with a given UCID.</summary>
        /// <param name="UCID">The UCID associated with the document</param>
        /// <returns></returns>
        public string GetFilePath(int UCID)
        {
            string value = "";
            try
            {
                if (fileNames.TryGetValue(UCID, out value))
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
                console.Append(Asys.GetTime() + "[ERROR]: FileInteraction.GetFileFromMap: Could not find instance!");
                MessageBox.Show("Error in FileInteraction.GetFileFromMap:\n" + ex.Message);
                return "";
            }

        }

        /// <summary>Returns the file type associated with a given UCID.</summary>
        /// <param name="UCID">The UCID associated with the document</param>
        /// <returns></returns>
        public EFileType GetFileType(int UCID)
        {
            EFileType value = EFileType.OTHER;

            try {
                if (fileTypes.TryGetValue(UCID, out value))
                {
                    return value;
                }
                else
                {
                    return EFileType.OTHER;
                }
            }
            catch (Exception ex)
            {
                console.Append(Asys.GetTime() + "[ERROR]: FileInteraction.GetFileType: Could not find instance!");
                MessageBox.Show("Error in FileInteraction.GetFileType:\n" + ex.Message);
                return EFileType.OTHER;
            }
        }

    }
}
