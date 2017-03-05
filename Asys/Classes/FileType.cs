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
    enum FileType
    {
        PlainText,     // Plain Text file
        RichText,      // Rich Text file
        JavaSrc,       // Java source file
        CSharpSrc,     // C# source file
        Other          // Any other file
    };
}
