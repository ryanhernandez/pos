param(
    [Parameter(ValueFromRemainingArguments=$true)]
    $RemainingArgs
)

Write-Host "Running solution tests: dotnet test .\pos.sln $RemainingArgs"
dotnet test .\pos.sln --nologo @RemainingArgs
exit $LASTEXITCODE
