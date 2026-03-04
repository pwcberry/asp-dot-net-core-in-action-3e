<#
.SYNOPSIS
Updates the SDK reference for each project in the sub-directories to .NET 10.
#>
$currentDir = $(Get-Location)
Write-Host "Loading package versions..." -ForegroundColor Yellow

$projects = get-childitem $currentDir -exclude ".gitignore" -include *.csproj -recurse | select-string "net7.0" | foreach -process { $a = $_ -split ':\d'; $a[0] }

$projects | foreach {
    $path = $_
    $shortName = $path.Replace($currentDir, "").TrimStart('\')
    Write-Host "> $shortName..." -ForegroundColor DarkCyan

    $xml = New-Object xml
    $xml.Load($path)
    $xml.Project.PropertyGroup.TargetFramework = "net10.0"
    $xml.Save($path)
    
    Write-Host "Changed" -ForegroundColor DarkGreen
    Write-Host ""
}

Write-Host "Completed." -ForegroundColor Green
