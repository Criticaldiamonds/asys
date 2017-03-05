using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsysEditor.Classes
{
    public class Strings
    {
        private Strings(string value) { Value = value; }

        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }

        public static Strings AsysPrefs_OLD { get { return new Strings("https://dl.dropboxusercontent.com/u/276558657/Asys/asys.a_prefs"); } }
        public static Strings AsysPrefs { get { return new Strings("https://dl.dropboxusercontent.com/u/276558657/Asys/asys.txt"); } }
    }
}
