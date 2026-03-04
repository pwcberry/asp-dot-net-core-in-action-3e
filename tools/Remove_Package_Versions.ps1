<#
.SYNOPSIS
Once packages are collected into `packages.xml`, remove the version attributes from project dependencies 
to use the centralized package management.
#>
$currentDir = $(Get-Location)
$packages = @{}

Write-Host "Loading package versions..." -ForegroundColor Yellow

$xml = New-Object xml
$xml.Load((Join-Path -Path $currentDir -ChildPath "Directory.Packages.props"))

if (($null -ne $xml.Project.ItemGroup) -and ($null -ne $xml.Project.ItemGroup.PackageVersion)) {
    $xml.Project.ItemGroup.PackageVersion | ? { $_ } | foreach {
        $include = $_.GetAttribute("Include")
        $version = $_.GetAttribute("Version")

        If (-Not($packages.Contains($include))) {
            $packages.Add($include, $version)
        }
    }
}

Write-Host "Displaying versions:" -ForegroundColor Cyan
$packages.GetEnumerator() | foreach {
    Write-Host ("  [{0}]: {1}" -f $_.Name, $_.Value)
}

Write-Host ""
Write-Host "Loading package versions..." -ForegroundColor Yellow
$projects = get-childitem -Path $currentDir -recurse -exclude ".github" -include *.csproj -file | Select-Object -ExpandProperty FullName

Write-Host "Begin updating project files..." -ForegroundColor Yellow

for($i = 0; $i -lt $projects.Count; $i++) {
    $path = $projects[$i]
    $shortName = $path.Replace($currentDir, "").TrimStart('\')
    Write-Host "> $shortName..."

    $xml = New-Object xml
    $xml.Load($path)
    $hasChanges = $False

    if (($null -ne $xml.Project.ItemGroup) -and ($null -ne $xml.Project.ItemGroup.PackageReference)) {
        $xml.Project.ItemGroup.PackageReference | ? { $_ } | foreach {
            $include = $_.GetAttribute("Include")
            $version = $_.GetAttribute("Version")

            If (($packages.Contains($include)) -And ($packages[$include] -eq $version)) {
                $_.RemoveAttribute("Version")
                If (-Not($hasChanges)) {
                    $hasChanges = $True
                }
            }
        }

        If ($hasChanges) {
            Write-Host "Changed" -ForegroundColor DarkCyan
            $xml.Save($path)
        } else {
            Write-Host "Skipped" -ForegroundColor DarkGreen
        }
        Write-Host ""
    }

    # $completed = ($i / $projects.Count) * 100;
    # Write-Progress -Activity "Updating" -Status "Progress:" -PercentComplete $completed
}

Write-Host "Completed." -ForegroundColor Green
