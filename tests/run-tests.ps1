$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$buildScript = Join-Path $root "tools\build.ps1"
$fixtureDir = Join-Path $PSScriptRoot "manual\basic"
$v1Exe = Join-Path $root "compiled\Version1\Debug\WordFrequencyAnalyzerV1.exe"
$v2Exe = Join-Path $root "compiled\Version2\Debug\WordFrequencyAnalyzerV2.exe"

function Assert-ExitCode {
    param(
        [int]$Actual,
        [int]$Expected,
        [string]$CaseName
    )

    if ($Actual -ne $Expected) {
        throw "$CaseName failed: expected exit code $Expected but got $Actual"
    }
}

function Assert-Frequency {
    param(
        [string[]]$Output,
        [string]$Word,
        [int]$Count,
        [string]$CaseName
    )

    $pattern = "^\s*" + [regex]::Escape($Word) + "\s+" + $Count + "\s*$"
    $match = $Output | Where-Object { $_ -match $pattern }
    if (-not $match) {
        throw "$CaseName failed: expected '$Word' to have frequency $Count"
    }
}

& powershell -ExecutionPolicy Bypass -File $buildScript
Assert-ExitCode $LASTEXITCODE 0 "Build"

$caseName = "Version 1 basic counting"
$output = & $v1Exe $fixtureDir
Assert-ExitCode $LASTEXITCODE 0 $caseName
Assert-Frequency $output "hello" 3 $caseName
Assert-Frequency $output "world" 3 $caseName
Assert-Frequency $output "car" 2 $caseName
Assert-Frequency $output "cars" 1 $caseName
Assert-Frequency $output "engine" 2 $caseName

$caseName = "Version 2 ending trimming"
$output = & $v2Exe $fixtureDir 3 1
Assert-ExitCode $LASTEXITCODE 0 $caseName
Assert-Frequency $output "car" 3 $caseName
Assert-Frequency $output "hell" 3 $caseName
Assert-Frequency $output "worl" 3 $caseName
Assert-Frequency $output "engin" 2 $caseName

$caseName = "Version 2 no trim below threshold"
$output = & $v2Exe $fixtureDir 6 2
Assert-ExitCode $LASTEXITCODE 0 $caseName
Assert-Frequency $output "hello" 3 $caseName
Assert-Frequency $output "world" 3 $caseName
Assert-Frequency $output "car" 2 $caseName
Assert-Frequency $output "cars" 1 $caseName
Assert-Frequency $output "engine" 2 $caseName

Write-Host "All tests passed."

