using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsysEditor.Classes
{
    /// <summary>
    /// Represents the file-type of a document
    /// </summary>
    enum EFileType
    {
        PLAIN_TEXT,           // Plain Text file
        RICH_TEXT,      // Rich Text file
        JAVA,           // Java source file
        CSHARP,         // C# source file
        OTHER           // Any other file
    };
}
