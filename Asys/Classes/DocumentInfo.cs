namespace AsysEditor.Classes
{
    /// <summary>
    /// Stores vital information about a document.
    /// </summary>
    class DocumentInfo
    {
        private string fileName;
        private int UCID;
        private FileType fileType;

        /// <summary>
        /// Store information about a document
        /// </summary>
        /// <param name="name">The name of the file</param>
        /// <param name="id"> The UCID of the object</param>
        /// <param name="type">The type of file</param>
        public DocumentInfo(string name, int id, FileType type)
        {
            this.fileName = name;
            this.UCID = id;
            this.fileType = type;
        }

        /// <summary>
        /// Generates a blank DocumentInfo
        /// </summary>
        public DocumentInfo() : this("", -1, FileType.Other) { }

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }

        public int ID
        {
            get
            {
                return UCID;
            }
            set
            {
                UCID = value;
            }
        }

        public FileType FileType
        {
            get
            {
                return fileType;
            }
            set
            {
                fileType = value;
            }
        }
    }
}
