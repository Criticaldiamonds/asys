namespace AsysEditor.Classes
{
    public class References
    {
        private References(string value) { Value = value; }

        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }

        public static References AsysPrefs_OLD { get { return new References("https://dl.dropboxusercontent.com/u/276558657/Asys/asys.a_prefs"); } }
        public static References AsysPrefs { get { return new References("https://dl.dropboxusercontent.com/u/276558657/Asys/asys.txt"); } }
    }
}
