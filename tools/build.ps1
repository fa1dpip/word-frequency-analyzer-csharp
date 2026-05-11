param(
    [ValidateSet("Version1")]
    [string]$Version = "Version1"
)

$ErrorActionPreference = "Stop"

$compiler = "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe"
if (-not (Test-Path $compiler)) {
    throw "C# compiler was not found at $compiler"
}

$root = Split-Path -Parent $PSScriptRoot
$sourceDir = Join-Path $root "src\$Version"
$outputDir = Join-Path $root "compiled\$Version\Debug"
$assemblyName = if ($Version -eq "Version1") { "WordFrequencyAnalyzerV1.exe" } else { "WordFrequencyAnalyzerV2.exe" }
$outputFile = Join-Path $outputDir $assemblyName

New-Item -ItemType Directory -Path $outputDir -Force | Out-Null

$sources = Get-ChildItem -Path $sourceDir -Filter *.cs | Sort-Object Name | ForEach-Object { $_.FullName }
& $compiler /nologo /debug:full /target:exe "/out:$outputFile" /reference:System.Core.dll $sources

if ($LASTEXITCODE -ne 0) {
    throw "Build failed for $Version"
}

Write-Host "Built $Version -> $outputDir"
