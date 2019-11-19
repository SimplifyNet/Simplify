using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Simplify.Templates
{
	/// <summary>
	/// Provides ITemplate fluent builder
	/// </summary>
	public class TemplateBuilder
	{
		private string? _text;
		private string? _filePath;

		private TemplateBuilder()
		{
		}

		/// <summary>
		/// Create builder based on the string.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public static TemplateBuilder FromString(string text)
		{
			return new TemplateBuilder
			{
				_text = text
			};
		}

		/// <summary>
		/// Create builder based on the file.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
		public static TemplateBuilder FromFile(string filePath)
		{
			return new TemplateBuilder
			{
				_filePath = filePath
			};
		}

		public static TemplateBuilder FromCurrentAssembly(string filePath)
		{
			throw new NotImplementedException();
		}

		public static TemplateBuilder FromAssembly(string filePath, Assembly assembly)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Builds the template.
		/// </summary>
		/// <returns></returns>
		public ITemplate Build()
		{
			BuildProcess();

			if (_text == null)
				throw new TemplateException("Can't initialize empty template");

			return new Template(_text);
		}

		/// <summary>
		/// Builds the template asynchronously.
		/// </summary>
		/// <returns></returns>
		public async Task<ITemplate> BuildAsync()
		{
			await BuildProcessAsync();

			if (_text == null)
				throw new TemplateException("Can't initialize empty template");

			return new Template(_text);
		}

		public TemplateBuilder Localizable(string language, string baseLanguage = "en")
		{
			throw new NotImplementedException();
		}

		public TemplateBuilder LocalizableFromCurrentThreadLanguage(string baseLanguage = "en")
		{
			throw new NotImplementedException();
		}

		public TemplateBuilder FixLineEndingsHtml()
		{
			throw new NotImplementedException();
		}

		private void BuildProcess()
		{
			if (_text != null)
				return;

			if (!string.IsNullOrEmpty(_filePath))
				_text = File.ReadAllText(_filePath);
		}

		private async Task BuildProcessAsync()
		{
			if (_text != null)
				return;

			if (!string.IsNullOrEmpty(_filePath))
				_text = await ReadFileAsync();
		}

		private async Task<string> ReadFileAsync()
		{
			using var sr = new StreamReader(_filePath);

			return await sr.ReadToEndAsync();
		}
	}
}