using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Simplify.Templates;

/// <summary>
/// Provide files reader
/// </summary>
public class FileReader
{
	/// <summary>
	/// Reads the file.
	/// </summary>
	/// <param name="filePath">The file path.</param>
	/// <returns></returns>
	public static string ReadFile(string filePath)
	{
		return File.ReadAllText(filePath);
	}

	/// <summary>
	/// Reads the file from assembly.
	/// </summary>
	/// <param name="filePath">The file path.</param>
	/// <param name="assembly">The assembly.</param>
	/// <returns></returns>
	/// <exception cref="TemplateException">Template: error loading file from resources in assembly '{assembly.FullName}': {filePath}</exception>
	public static string ReadFromAssembly(string filePath, Assembly assembly)
	{
		using var fileStream = assembly.GetManifestResourceStream(filePath);

		if (fileStream == null)
			throw new TemplateException($"Error loading file from the ssembly '{assembly.FullName}': {filePath}");

		using var sr = new StreamReader(fileStream);

		return sr.ReadToEnd();
	}

	/// <summary>
	/// Reads from assembly asynchronous.
	/// </summary>
	/// <param name="filePath">The file path.</param>
	/// <param name="assembly">The assembly.</param>
	/// <returns></returns>
	/// <exception cref="TemplateException">Template: error loading file from resources in assembly '{assembly.FullName}': {filePath}</exception>
	public static async Task<string> ReadFromAssemblyAsync(string filePath, Assembly assembly)
	{
		using var fileStream = assembly.GetManifestResourceStream(filePath);

		if (fileStream == null)
			throw new TemplateException($"Error loading file from the assembly '{assembly.FullName}': {filePath}");

		using var sr = new StreamReader(fileStream);

		return await sr.ReadToEndAsync();
	}

	/// <summary>
	/// Reads the file asynchronously.
	/// </summary>
	/// <param name="filePath">The file path.</param>
	/// <returns></returns>
	internal static async Task<string> ReadFileAsync(string filePath)
	{
		using var sr = new StreamReader(filePath);

		return await sr.ReadToEndAsync();
	}
}