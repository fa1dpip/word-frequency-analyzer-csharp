using System;
using System.Collections.Generic;

namespace WordFrequencyAnalyzer.Version1
{
    public class WordCounter
    {
        private readonly Dictionary<string, int> frequencies;

        public WordCounter()
        {
            frequencies = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        }

        public void AddWords(IEnumerable<string> words)
        {
            foreach (string word in words)
            {
                AddWord(word);
            }
        }

        private void AddWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                return;
            }

            if (frequencies.ContainsKey(word))
            {
                frequencies[word]++;
            }
            else
            {
                frequencies[word] = 1;
            }
        }

        public Dictionary<string, int> GetFrequencies()
        {
            return new Dictionary<string, int>(frequencies, StringComparer.OrdinalIgnoreCase);
        }
    }
}

