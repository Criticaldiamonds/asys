using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

using AsysEditor.Forms;

namespace AsysEditor.Classes
{
    class DocumentInteraction
    {

        public void Undo(RichTextBoxPrintCtrl.RichTextBoxPrintCtrl rtbIn)
        {
            rtbIn.Undo();
        }

        public void Redo(RichTextBoxPrintCtrl.RichTextBoxPrintCtrl rtbIn)
        {
            rtbIn.Redo();
        }

        public void Cut(RichTextBoxPrintCtrl.RichTextBoxPrintCtrl rtbIn)
        {
            rtbIn.Cut();
        }

        public void Copy(RichTextBoxPrintCtrl.RichTextBoxPrintCtrl rtbIn)
        {
            rtbIn.Copy();
        }

        public void Paste(RichTextBoxPrintCtrl.RichTextBoxPrintCtrl rtbIn)
        {
            rtbIn.Paste();
        }

        public void SelectAll(RichTextBoxPrintCtrl.RichTextBoxPrintCtrl rtbIn)
        {
            rtbIn.SelectAll();
        }

    }
}
