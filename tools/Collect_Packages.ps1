<#
.SYNOPSIS
Retrieve references to all packages for all projects found in the sub-directories.
#>

# Get a list of the fullnames of all CSPROJ files
Write-Host "Retrieving *.csproj..." -ForegroundColor Yellow
$currentDir = $(Get-Location)
$projects = get-childitem -Path $currentDir -recurse -exclude ".github" -include *.csproj -file | Select-Object -ExpandProperty FullName
$packages = @{}

Write-Host "Extracting packages from each *.csproj..." -ForegroundColor Yellow
$projects | foreach {
    $path = $_
    $shortName = $path.Replace($currentDir, "").TrimStart('\')
    Write-Host "> $shortName..."

    $xml = New-Object xml
    $xml.Load($path)

    if (($null -ne $xml.Project.ItemGroup) -and ($null -ne $xml.Project.ItemGroup.PackageReference)) {
        $xml.Project.ItemGroup.PackageReference | ? { $_ } | foreach {
            $include = $_.GetAttribute("Include")
            $version = $_.GetAttribute("Version")

            If ((-Not($packages.Contains($include))) -And (-Not([String]::IsNullOrEmpty($version)))) {
                $packages.Add($include, $version)
            } else {
                # Ensure to register the latest major version of the package
                $v1 = ($packages[$include] -split "\.")[0] -as [int] ?? 0
                $v2 = ($version -split "\.")[0] -as [int] ?? 0
                If ($v2 -gt $v1) {
                    Write-Host ("?? [{0}]: {1} ({2})" -f $include, $version, $packages[$include]) -ForegroundColor DarkYellow
                    $packages[$include] = $version
                }
            }
        }
    }
}

Write-Host "Preparing XML output..." -ForegroundColor Yellow
$output = New-Object xml
$root = $output.CreateElement("Project")
$output.AppendChild($root) | Out-Null

$propertyGroup = $output.CreateElement("PropertyGroup")
$comment = $output.CreateComment("Enable central package management, https://learn.microsoft.com/en-us/nuget/consume-packages/Central-Package-Management")
$propertyGroup.AppendChild($comment) | Out-Null

$central = $output.CreateElement("ManagePackageVersionsCentrally")
$central.InnerText = "true"
$propertyGroup.AppendChild($central) | Out-Null
$root.AppendChild($propertyGroup) | Out-Null

$itemGroup = $output.CreateElement("ItemGroup")
$root.AppendChild($itemGroup) | Out-Null

# Write-Host $packages
$packages.GetEnumerator() | foreach {
    $p = $output.CreateElement("PackageVersion")
    $p.SetAttribute("Include", $_.Name)
    $p.SetAttribute("Version", $_.Value)
    $itemGroup.AppendChild($p) | Out-Null
}

Write-Host "Writing output..." -ForegroundColor Yellow

$output.Save((Join-Path -Path $(Get-Location) -ChildPath "packages.xml"))

Write-Host "Completed." -ForegroundColor Green
