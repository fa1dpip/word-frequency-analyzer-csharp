# Word Frequency Analyzer

C# command-line application for reading all `.txt` files from a directory and counting word frequencies.

## Version 1

Basic case-insensitive word counting:

- scans a selected directory for `.txt` files;
- reads each file;
- splits text by whitespace and punctuation;
- converts words to lowercase;
- counts words with `Dictionary<string, int>`;
- prints results to the console.

## Build

This project is written in C# and can be compiled with the .NET Framework C# compiler:

```powershell
.\tools\build.ps1
```

## Run

```powershell
.\compiled\Version1\Debug\WordFrequencyAnalyzerV1.exe "C:\Path\To\TxtFiles"
```

Use `--recursive` to include subdirectories.

