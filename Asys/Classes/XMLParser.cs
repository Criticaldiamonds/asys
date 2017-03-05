using System;
using System.Collections.Generic;

using System.Text.RegularExpressions;

namespace AsysEditor.Classes
{
    /// <summary>
    /// Contains the methods needed to parse a simple XML file
    /// </summary>
    class XMLParser
    {
        /// <summary>
        /// Parses a simple XML file.
        /// </summary>
        /// <remarks>
        /// Does NOT support nested tags.
        /// </remarks>
        /// <param name="xml">The file to parse</param>
        /// <param name="tag">The wanted value</param>
        /// <param name="clean">Remove whitespace</param>
        /// <param name="replaceNewLines">Replace "[-n]" with "\n\r"</param>
        /// <returns></returns>
        public static string Parse(string xml, string tag, bool clean, bool replaceNewLines)
        {
            if (xml == String.Empty || tag == String.Empty) { return "error"; }
            if (!(xml.Contains("<" + tag + ">"))) { return "error"; }

            // Get all XML tags: <tag>
            string _tag = "\\<(.*?)\\>";
            MatchCollection tagMatches = new Regex(_tag).Matches(xml);

            List<string> tags = new List<string>();

            // Add the tag to a list
            foreach (Match m in tagMatches)
            {
                // Clean the tag and add it to the list
                tags.Add(m.Groups[1].Value.Replace("<", string.Empty).Replace(">", string.Empty));
            }

            // Get the value of the tag
            foreach (string h in tags)
            {
                if (!h.Equals(tag)) continue;

                string head = "\\<" + h + "\\>";
                string foot = "\\</" + h + "\\>";
                
                string contents = new Regex(head + "(.*?)" + foot).Match(xml).Groups[1].Value;

                // Clean the result if nessesary
                if (clean) return contents.Trim();
                else if (replaceNewLines) return contents = Regex.Replace(contents, "\\[-n\\]", "\r\n");
                else return contents;
            }

            return "error";
        }

    }
}
