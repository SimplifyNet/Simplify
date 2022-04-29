# Patching all sources with internal attribute for use in source packages

$files = get-childitem . *.cs -rec
foreach ($file in $files)
{
	(Get-Content $file.PSPath) |
	Foreach-Object { $_ -replace "public static class", "internal static class" } |
	Foreach-Object { $_ -replace "public sealed class", "internal sealed class" } |
	Foreach-Object { $_ -replace "public class", "internal class" } |
	Foreach-Object { $_ -replace "public interface", "internal interface" } |
	Set-Content -Encoding UTF8 $file.PSPath
}

# Getting packages versions

$xmlVersion = [System.Diagnostics.FileVersionInfo]::GetVersionInfo("$PSScriptRoot/src/Simplify.Xml/bin/Any CPU/Release/netstandard2.0/Simplify.Xml.dll").FileVersion
$stringVersion = [System.Diagnostics.FileVersionInfo]::GetVersionInfo("$PSScriptRoot/src/Simplify.String/bin/Any CPU/Release/net452/Simplify.String.dll").FileVersion
$systemVersion = [System.Diagnostics.FileVersionInfo]::GetVersionInfo("$PSScriptRoot/src/Simplify.System/bin/Any CPU/Release/netstandard2.0/Simplify.System.dll").FileVersion

# Packing source packages

src\.nuget\NuGet.exe pack src/Simplify.Xml/Simplify.Xml.Sources.nuspec -Version $xmlVersion -OutputDirectory ./src/publish/
src\.nuget\NuGet.exe pack src/Simplify.String/Simplify.String.Sources.nuspec -Version $stringVersion -OutputDirectory ./src/publish/
src\.nuget\NuGet.exe pack src/Simplify.System/Simplify.System.Sources.nuspec -Version $systemVersion -OutputDirectory ./src/publish/
