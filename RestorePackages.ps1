# Dependecies
$packageConfigs = Get-ChildItem . -Recurse | where{$_.Name -eq "packages.config"}
foreach($packageConfig in $packageConfigs){
	Write-Host "Restoring" $packageConfig.FullName 
	nuget i $packageConfig.FullName -o Packages
}
