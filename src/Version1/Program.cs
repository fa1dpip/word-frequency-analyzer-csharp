using System;
using System.Collections.Generic;

namespace WordFrequencyAnalyzer.Version1
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

            if (args.Length > 2)
            {
                Console.Error.WriteLine("Too many arguments.");
                PrintUsage();
                return 1;
            }

            string directoryPath = args[0];
            bool recursive = args.Length == 2 && IsRecursiveFlag(args[1]);

            if (args.Length == 2 && !recursive)
            {
                Console.Error.WriteLine("Unknown option: " + args[1]);
                PrintUsage();
                return 1;
            }

            try
            {
                FileReader fileReader = new FileReader();
                TextProcessor textProcessor = new TextProcessor();
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
            Console.WriteLine("Word Frequency Analyzer - Version 1");
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("  WordFrequencyAnalyzerV1.exe <directoryPath> [--recursive]");
            Console.WriteLine();
            Console.WriteLine("Example:");
            Console.WriteLine("  WordFrequencyAnalyzerV1.exe \"C:\\TextFiles\"");
        }
    }
}

