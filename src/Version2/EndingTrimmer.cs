namespace WordFrequencyAnalyzer.Version2
{
    public class EndingTrimmer
    {
        private readonly int lengthThreshold;
        private readonly int charactersToRemove;

        public EndingTrimmer(int lengthThreshold, int charactersToRemove)
        {
            this.lengthThreshold = lengthThreshold;
            this.charactersToRemove = charactersToRemove;
        }

        public string Normalize(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                return string.Empty;
            }

            if (word.Length <= lengthThreshold || charactersToRemove == 0)
            {
                return word;
            }

            if (charactersToRemove >= word.Length)
            {
                return string.Empty;
            }

            return word.Substring(0, word.Length - charactersToRemove);
        }
    }
}

