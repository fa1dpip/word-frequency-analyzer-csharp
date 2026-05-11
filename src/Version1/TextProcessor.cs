using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WordFrequencyAnalyzer.Version1
{
    public class TextProcessor
    {
        private static readonly Regex WordSeparator = new Regex(@"[\s\p{P}]+", RegexOptions.Compiled);

        public IEnumerable<string> SplitIntoWords(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                yield break;
            }

            string[] words = WordSeparator.Split(text.ToLowerInvariant());
            foreach (string word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    yield return word;
                }
            }
        }
    }
}

