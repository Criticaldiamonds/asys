using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Text.RegularExpressions;

using AsysEditor.Forms;

namespace AsysEditor.Classes
{
    /// <summary>
    /// Loads a file and retrives a string from said file from the Internet
    /// </summary>
    class ExternalStringParser
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
                Asys.console.Append(Asys.GetTime() + "[ERROR]: ExternalStringParser.Load: Could not load!");
            }
        }

        /// <summary>
        /// Gets the value of the specified key
        /// File must be in the format 'KEY=VALUE" with no space seperating the key and the value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetKey(string key)
        {
            if (str == String.Empty) Asys.console.Append(Asys.GetTime() + "[ERROR]: ExternalStringParser.GetKey: String Empty!");
            if (str == String.Empty) return "error";

            string[] lines = str.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach(string curLine in lines)
            {
                // Get the line needed
                if (curLine.StartsWith(key))
                {
                    string[] substrings = curLine.Split('=');

                    for (int i = 0; i < substrings.Length; i++)
                    {
                        string t = substrings[i];
                        if (t.ToUpper().Equals(key.ToUpper()))
                        {
                            string r = substrings[i + 1];
                            return r;
                        }
                    }

                    break;
                }
            }

            return "error";
        }

        /// <summary>
        /// Returns the value of the specified key.
        /// The key must be in XML format: "<key>RESULT</key>"
        /// </summary>
        /// <param name="key">Token being parsed</param>
        /// <returns></returns>
        public string ParseKey(string key)
        {
            // Make sure that the string getting parsed isn't empty
            if (str == String.Empty) { Asys.console.Append(Asys.GetTime() + "[ERROR]: ExternalStringParser.ParseKey: String empty"); return "error"; }

            if (str.Contains(key))
            {
                // Matching format: <key>RESULT</key>
                Match match = new Regex("\\<" + key + "\\>" + "(.*?)" + "\\</" + key + "\\>").Match(str);
                return match.Groups[1].Value;
            }

            return "error";
        }

    }
}
