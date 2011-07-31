# Dependecies
$packageConfigs = Get-ChildItem . -Recurse | where{$_.Name -eq "packages.config"}
foreach($packageConfig in $packageConfigs){
	Write-Host "Restoring" $packageConfig.FullName 
	.\Tools\nuget.exe i $packageConfig.FullName -o Packages
}
