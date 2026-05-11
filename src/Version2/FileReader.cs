using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WordFrequencyAnalyzer.Version2
{
    public class FileReader
    {
        public IEnumerable<string> ReadTextFiles(string directoryPath, bool recursive)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                throw new ArgumentException("Directory path is required.");
            }

            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException("Directory does not exist: " + directoryPath);
            }

            SearchOption searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            string[] files = Directory.GetFiles(directoryPath, "*.txt", searchOption);
            Array.Sort(files, StringComparer.OrdinalIgnoreCase);

            foreach (string file in files)
            {
                yield return File.ReadAllText(file, Encoding.UTF8);
            }
        }
    }
}

