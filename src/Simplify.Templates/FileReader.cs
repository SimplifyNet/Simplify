using System.IO;
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
	}
}