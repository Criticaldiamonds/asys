using System;
using System.Net;

using AsysEditor.Forms;

namespace AsysEditor.Classes
{
    /// <summary>
    /// Loads a file and retrives a string from said file from the Internet
    /// </summary>
    class WebString
    {
        /// <summary>
        /// Loads the specified file from the Internet
        /// </summary>
        /// <param name="path">Path to the string</param>
        public static string Load(string path)
        {
            try {
                using (WebClient client = new WebClient()) {
                    return client.DownloadString(path);
                }
            }
            catch (Exception) {
                Asys.console.Append(Asys.GetTime() + "[ERROR]: ExternalStringParser.Load: Could not load!");
                return "error";
            }
        }
    }
}
