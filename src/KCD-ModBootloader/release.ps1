$VERSION = '1.00'
$targetDir = "C:\Program Files (x86)\Steam\steamapps\common\KingdomComeDeliverance\mods\KCD_Bootloader\"
$srcDir = "$PSScriptRoot\src"

(Get-Content "$PSScriptRoot\mod.manifesttemplate") | Foreach-Object {
	$_ -replace '{created}',  [datetime]::Now.ToString('ddd MMM dd HH:mm:ss yyyy', [Globalization.CultureInfo]::CreateSpecificCulture('en-GB')) `
		-replace '{version}', $VERSION
} | Out-File "$PSScriptRoot\mod.manifest"

New-Item $targetDir -ItemType Directory -Force | Out-Null
Remove-Item "$targetDir\Data" -Recurse

New-Item "$targetDir\Data" -ItemType Directory -Force | Out-Null
Compress-Archive (Get-ChildItem $srcDir | Select-Object -ExpandProperty FullName) -DestinationPath "$targetDir\Data\data.zip" -Force
Rename-Item "$targetDir\Data\data.zip" -NewName "$targetDir\Data\data.pak" -Force

Move-Item "$PSScriptRoot\mod.manifest" -Destination $targetDir -Force