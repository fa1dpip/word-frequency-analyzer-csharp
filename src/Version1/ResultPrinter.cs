using System;
using System.Collections.Generic;
using System.Linq;

namespace WordFrequencyAnalyzer.Version1
{
    public class ResultPrinter
    {
        public void Print(Dictionary<string, int> frequencies)
        {
            if (frequencies.Count == 0)
            {
                Console.WriteLine("No words found.");
                return;
            }

            Console.WriteLine("{0,-25} {1,10}", "Word", "Frequency");
            Console.WriteLine(new string('-', 38));

            foreach (KeyValuePair<string, int> entry in frequencies
                .OrderByDescending(item => item.Value)
                .ThenBy(item => item.Key))
            {
                Console.WriteLine("{0,-25} {1,10}", entry.Key, entry.Value);
            }
        }
    }
}

