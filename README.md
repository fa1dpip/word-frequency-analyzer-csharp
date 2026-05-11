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

## Version 2

Extended word counting with ending trimming:

- performs all Version 1 steps;
- if a word length is greater than `N`, removes the last `M` characters before counting;
- ignores empty words after splitting or trimming.

## Build

This project is written in C# and can be compiled with the .NET Framework C# compiler:

```powershell
.\tools\build.ps1
```

## Run

Version 1:

```powershell
.\compiled\Version1\Debug\WordFrequencyAnalyzerV1.exe "C:\Path\To\TxtFiles"
```

Version 2:

```powershell
.\compiled\Version2\Debug\WordFrequencyAnalyzerV2.exe "C:\Path\To\TxtFiles" 6 2
```

Use `--recursive` to include subdirectories.

## Repository Versions

- `version-1` tag: basic implementation.
- `version-2` tag: extended implementation with `N` and `M` trimming.
