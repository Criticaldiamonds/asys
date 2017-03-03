using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsysEditor.Classes
{
    public class EStrings
    {
        private EStrings(string value) { Value = value; }

        private string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }

        public static EStrings AsysPrefs_OLD { get { return new EStrings("https://dl.dropboxusercontent.com/u/276558657/Asys/asys.a_prefs"); } }
        public static EStrings AsysPrefs_NEW { get { return new EStrings("https://dl.dropboxusercontent.com/u/276558657/Asys/asys.txt"); } }
    }
}
