using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace Asys
{
    class AsysExternalStringParser
    {
        string str = String.Empty;

        /// <summary>
        /// Loads the specified file from the Internet
        /// </summary>
        /// <param name="path"></param>
        public void Load(string path)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    str = client.DownloadString(path);
                }
            }
            catch (Exception)
            {
                Asys.console.Append(Asys.GetTime() + "[ERROR]: AsysExternalStringParser.Load: Could not load!");
            }
        }

        /// <summary>
        /// Gets the value of the specified key
        /// File must be in the format 'KEY=VALUE" with no space seperating the key and the value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ParseString(string key)
        {
            if (str == String.Empty) Asys.console.Append(Asys.GetTime() + "[ERROR]: AsysExternalStringParser.ParseString: String Empty!");
            if (str == String.Empty) return "error";

            string[] substrings = str.Split('=');

            for (int i = 0; i < substrings.Length; i++)
            {
                string t = substrings[i];
                if (t.ToUpper().Equals(key.ToUpper()))
                {
                    string r = substrings[i + 1];
                    return r;
                }
            }
            return "error";
        }
    }
}
