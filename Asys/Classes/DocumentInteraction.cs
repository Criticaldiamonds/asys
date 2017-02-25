using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

using AsysEditor.Forms;

using AsysControls;

namespace AsysEditor.Classes
{
    class DocumentInteraction
    {

        public void Undo(RichTextBoxPrintCtrl rtbIn)
        {
            rtbIn.Undo();
        }

        public void Redo(RichTextBoxPrintCtrl rtbIn)
        {
            rtbIn.Redo();
        }

        public void Cut(RichTextBoxPrintCtrl rtbIn)
        {
            rtbIn.Cut();
        }

        public void Copy(RichTextBoxPrintCtrl rtbIn)
        {
            rtbIn.Copy();
        }

        public void Paste(RichTextBoxPrintCtrl rtbIn)
        {
            rtbIn.Paste();
        }

        public void SelectAll(RichTextBoxPrintCtrl rtbIn)
        {
            rtbIn.SelectAll();
        }

    }
}
