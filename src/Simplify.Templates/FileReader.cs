using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Simplify.Templates
{
	internal class FileReader
	{
		internal static string ReadFile(string filePath)
		{
			return File.ReadAllText(filePath);
		}

		internal static async Task<string> ReadFileAsync(string filePath)
		{
			using var sr = new StreamReader(filePath);

			return await sr.ReadToEndAsync();
		}

		internal static string ReadFromAssembly(string filePath, Assembly assembly)
		{
			var a = assembly.GetManifestResourceNames();
			using var fileStream = assembly.GetManifestResourceStream(filePath);

			if (fileStream == null)
				throw new TemplateException($"Template: error loading file from resources in assembly '{assembly.FullName}': {filePath}");

			using var sr = new StreamReader(fileStream);

			return sr.ReadToEnd();
		}

		internal static async Task<string> ReadFromAssemblyAsync(string filePath, Assembly assembly)
		{
			using var fileStream = assembly.GetManifestResourceStream(filePath);

			if (fileStream == null)
				throw new TemplateException($"Template: error loading file from resources in assembly '{assembly.FullName}': {filePath}");

			using var sr = new StreamReader(fileStream);

			return await sr.ReadToEndAsync();
		}
	}
}