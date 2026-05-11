using System;
using System.Collections.Generic;

namespace WordFrequencyAnalyzer.Version2
{
    public class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0 || HasHelpFlag(args))
            {
                PrintUsage();
                return args.Length == 0 ? 1 : 0;
            }

            if (args.Length < 3 || args.Length > 4)
            {
                Console.Error.WriteLine("Invalid number of arguments.");
                PrintUsage();
                return 1;
            }

            string directoryPath = args[0];
            int lengthThreshold;
            int charactersToRemove;

            if (!int.TryParse(args[1], out lengthThreshold) || lengthThreshold < 0)
            {
                Console.Error.WriteLine("N must be a non-negative integer.");
                return 1;
            }

            if (!int.TryParse(args[2], out charactersToRemove) || charactersToRemove < 0)
            {
                Console.Error.WriteLine("M must be a non-negative integer.");
                return 1;
            }

            bool recursive = args.Length == 4 && IsRecursiveFlag(args[3]);
            if (args.Length == 4 && !recursive)
            {
                Console.Error.WriteLine("Unknown option: " + args[3]);
                PrintUsage();
                return 1;
            }

            try
            {
                FileReader fileReader = new FileReader();
                EndingTrimmer endingTrimmer = new EndingTrimmer(lengthThreshold, charactersToRemove);
                TextProcessor textProcessor = new TextProcessor(endingTrimmer);
                WordCounter wordCounter = new WordCounter();
                ResultPrinter resultPrinter = new ResultPrinter();

                IEnumerable<string> contents = fileReader.ReadTextFiles(directoryPath, recursive);
                foreach (string content in contents)
                {
                    wordCounter.AddWords(textProcessor.SplitIntoWords(content));
                }

                resultPrinter.Print(wordCounter.GetFrequencies());
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error: " + ex.Message);
                return 1;
            }
        }

        private static bool HasHelpFlag(string[] args)
        {
            foreach (string arg in args)
            {
                if (arg == "--help" || arg == "-h" || arg == "/?")
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsRecursiveFlag(string arg)
        {
            return arg == "--recursive" || arg == "-r";
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Word Frequency Analyzer - Version 2");
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("  WordFrequencyAnalyzerV2.exe <directoryPath> <N> <M> [--recursive]");
            Console.WriteLine();
            Console.WriteLine("Rules:");
            Console.WriteLine("  If word length is greater than N, remove the last M characters.");
            Console.WriteLine();
            Console.WriteLine("Example:");
            Console.WriteLine("  WordFrequencyAnalyzerV2.exe \"C:\\TextFiles\" 6 2");
        }
    }
}

