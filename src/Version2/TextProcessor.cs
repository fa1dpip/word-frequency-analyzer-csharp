using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WordFrequencyAnalyzer.Version2
{
    public class TextProcessor
    {
        private static readonly Regex WordSeparator = new Regex(@"[\s\p{P}]+", RegexOptions.Compiled);
        private readonly EndingTrimmer endingTrimmer;

        public TextProcessor(EndingTrimmer endingTrimmer)
        {
            this.endingTrimmer = endingTrimmer;
        }

        public IEnumerable<string> SplitIntoWords(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                yield break;
            }

            string[] words = WordSeparator.Split(text.ToLowerInvariant());
            foreach (string word in words)
            {
                string normalizedWord = endingTrimmer.Normalize(word);
                if (!string.IsNullOrWhiteSpace(normalizedWord))
                {
                    yield return normalizedWord;
                }
            }
        }
    }
}

