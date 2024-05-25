using System.IO;
using System.Linq;
using System.Reflection;

namespace Simplify.Templates;

/// <summary>
/// Provides the file utility class
/// </summary>
public static class FileUtil
{
	/// <summary>
	/// Constructs the full file path based on calling assembly location.
	/// </summary>
	/// <param name="localFilePath">The local file path.</param>
	public static string ConstructFullFilePath(string localFilePath) => $"{Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)}/{localFilePath}";

	/// <summary>
	/// Constructs the path of the file located inside the assembly.
	/// </summary>
	/// <param name="filePath">The file path.</param>
	/// <param name="assembly">The assembly.</param>
	public static string ConstructAssemblyFilePath(string filePath, Assembly assembly)
	{
		filePath = filePath.Replace("/", ".");
		filePath = $"{assembly.GetName().Name}.{filePath}";

		return filePath;
	}

	/// <summary>
	/// Checks that Assemblies the file exists.
	/// </summary>
	/// <param name="filePath">The file path.</param>
	/// <param name="assembly">The assembly.</param>
	public static bool AssemblyFileExists(string filePath, Assembly assembly) =>
		assembly.GetManifestResourceNames().Contains(filePath);
}