using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using System.IO;
using System.Threading;

namespace AsysEditor.Classes
{
    class SpellCheck
    {
        private Dictionary<String, int> dictionary = new Dictionary<string, int>();
        private static Regex _wordRegex = new Regex("[a-z]+", RegexOptions.Compiled);

        public SpellCheck()
        {
            ;
        }

        public void Init()
        {
            Thread thread = new Thread(delegate()
                {
                    string fileContext = Properties.Resources.smaller;
                    List<string> wordList = fileContext.Split(new string[] { "\n", " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    foreach (var word in wordList)
                    {
                        string trimmedWord = word.Trim().ToLower();
                        if (_wordRegex.IsMatch(trimmedWord))
                        {
                            if (dictionary.ContainsKey(trimmedWord))
                                dictionary[trimmedWord]++;
                            else
                                dictionary.Add(trimmedWord, 1);
                        }
                    }
                });
            thread.Start();

        }

        public string Correct(string word)
        {
            if (string.IsNullOrEmpty(word)) return word;

            word = word.ToLower();

            if (dictionary.ContainsKey(word)) return word;

            List<string> list = Edits(word);
            Dictionary<string, int> candidates = new Dictionary<string, int>();

            foreach (string wordVariation in list)
            {
                if (dictionary.ContainsKey(wordVariation) && !candidates.ContainsKey(wordVariation))
                    candidates.Add(wordVariation, dictionary[wordVariation]);
            }

            if (candidates.Count > 0)
                return candidates.OrderByDescending(x => x.Value).First().Key;

            foreach (string item in list)
            {
                foreach (string wordVariation in Edits(item))
                {
                    if (dictionary.ContainsKey(wordVariation) && !candidates.ContainsKey(wordVariation))
                        candidates.Add(wordVariation, dictionary[wordVariation]);
                }
            }

            return (candidates.Count > 0) ? candidates.OrderByDescending(x => x.Value).First().Key : word;
        }

        private List<string> Edits(string word)
        {
            var splits = new List<Tuple<string, string>>();
            var transposes = new List<string>();
            var deletes = new List<string>();
            var replaces = new List<string>();
            var inserts = new List<string>();

            // Splits
            for (int i = 0; i < word.Length; i++)
            {
                var tuple = new Tuple<string, string>(word.Substring(0, i), word.Substring(i));
                splits.Add(tuple);
            }

            // Deletes
            for (int i = 0; i < splits.Count; i++)
            {
                string a = splits[i].Item1;
                string b = splits[i].Item2;

                if (!string.IsNullOrEmpty(b))
                {
                    deletes.Add(a + b.Substring(1));
                }
            }

            // Replaces
            for (int i = 0; i < splits.Count; i++)
            {
                string a = splits[i].Item1;
                string b = splits[i].Item2;

                if (!string.IsNullOrEmpty(b))
                {
                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        replaces.Add(a + c + b.Substring(1));
                    }
                }
            }

            // Inserts
            for (int i = 0; i < splits.Count; i++)
            {
                string a = splits[i].Item1;
                string b = splits[i].Item2;

                for (char c = 'a'; c <= 'z'; c++)
                {
                    inserts.Add(a + c + b);
                }
            }

            // Combine all the results
            return deletes.Union(transposes).Union(replaces).Union(inserts).ToList();
        }

    }
}

