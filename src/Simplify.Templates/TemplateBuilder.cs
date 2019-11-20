using System;
using System.IO;
using System.Reflection;
using System.Threading;
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
		private Assembly? assembly;
		private string? _language;
		private string? _baseLanguage;
		private bool _fixLineEndingsHtml;

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
			if (text == null)
				throw new ArgumentNullException(nameof(text));

			return new TemplateBuilder
			{
				_text = text
			};
		}

		/// <summary>
		/// Create builder based on the file path, accepted full path or relative path according to current working directory.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
		public static TemplateBuilder FromFile(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
				throw new ArgumentException("Value cannot be null or empty.", nameof(filePath));

			FileExistenceCheck(filePath);

			return new TemplateBuilder
			{
				_filePath = filePath
			};
		}

		/// <summary>
		/// Create builder based on the file using calling assembly path prefix in filePath.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
		public static TemplateBuilder FromLocalFile(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
				throw new ArgumentException("Value cannot be null or empty.", nameof(filePath));

			filePath = $"{Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)}/{filePath}";

			FileExistenceCheck(filePath);

			return new TemplateBuilder
			{
				_filePath = filePath
			};
		}

		/// <summary>
		/// Creates builder based on specified assembly embedded file, filePath should be path of the file inside assembly.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <param name="assembly">The assembly.</param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public static TemplateBuilder FromAssembly(string filePath, Assembly assembly)
		{
			if (string.IsNullOrEmpty(filePath))
				throw new ArgumentException("Value cannot be null or empty.", nameof(filePath));

			if (assembly == null)
				throw new ArgumentNullException(nameof(assembly));

			FileExistenceCheck(filePath);

			return new TemplateBuilder
			{
				_filePath = filePath,
				assembly = assembly
			};
		}

		/// <summary>
		/// Creates builder based on the calling assembly embedded file, filePath should be path of the file inside assembly.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public static TemplateBuilder FromCurrentAssembly(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
				throw new ArgumentException("Value cannot be null or empty.", nameof(filePath));

			FileExistenceCheck(filePath);

			return new TemplateBuilder
			{
				_filePath = filePath,
				assembly = Assembly.GetCallingAssembly()
			};
		}

		/// <summary>
		/// Builds the template.
		/// </summary>
		/// <returns></returns>
		public ITemplate Build()
		{
			var text = LoadTemplateText();

			if (text == null)
				throw new TemplateException("Can't initialize empty template");

			text = PreprocessTemplateText(text);

			var tpl = new Template(text);

			PostprocessTemplate(tpl);

			return tpl;
		}

		/// <summary>
		/// Builds the template asynchronously.
		/// </summary>
		/// <returns></returns>
		public async Task<ITemplate> BuildAsync()
		{
			var text = await LoadTemplateTextAsync();

			if (text == null)
				throw new TemplateException("Can't initialize empty template");

			PreprocessTemplateText(text);

			var tpl = new Template(text);

			await PostprocessTemplateAsync(tpl);

			return tpl;
		}

		/// <summary>
		/// Localizes the template from xml localization files.
		/// </summary>
		/// <param name="language">The language.</param>
		/// <param name="baseLanguage">The base language.</param>
		/// <returns></returns>
		public TemplateBuilder Localizable(string language, string baseLanguage = "en")
		{
			if (string.IsNullOrEmpty(language))
				throw new ArgumentException("Value cannot be null or empty.", nameof(language));

			if (string.IsNullOrEmpty(baseLanguage))
				throw new ArgumentException("Value cannot be null or empty.", nameof(baseLanguage));

			_language = language;
			_baseLanguage = baseLanguage;

			return this;
		}

		/// <summary>
		/// Localizes the template from xml localization files using Thread.CurrentThread culture language as default.
		/// </summary>
		/// <param name="baseLanguage">The base language.</param>
		/// <returns></returns>
		public TemplateBuilder LocalizableFromCurrentThreadLanguage(string baseLanguage = "en")
		{
			if (string.IsNullOrEmpty(baseLanguage))
				throw new ArgumentException("Value cannot be null or empty.", nameof(baseLanguage));

			_language = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
			_baseLanguage = baseLanguage;

			return this;
		}

		/// <summary>
		/// Replace all caret return characters by html <![CDATA[<BR />]]> tag.
		/// </summary>
		/// <returns></returns>
		public TemplateBuilder FixLineEndingsHtml()
		{
			_fixLineEndingsHtml = true;

			return this;
		}

		private static void FileExistenceCheck(string filePath)
		{
			if (!File.Exists(filePath))
				throw new FileNotFoundException($"Template file not found: {filePath}");
		}

		private string LoadTemplateText()
		{
			if (_text != null)
				return _text;

			if (!string.IsNullOrEmpty(_filePath))
				return FileReader.ReadFile(_filePath);

			throw new InvalidOperationException();
		}

		private async Task<string> LoadTemplateTextAsync()
		{
			if (_text != null)
				return _text;

			if (!string.IsNullOrEmpty(_filePath))
				return await FileReader.ReadFileAsync(_filePath);

			throw new InvalidOperationException();
		}

		private string PreprocessTemplateText(string text)
		{
			return _fixLineEndingsHtml ? text.Replace(Environment.NewLine, "<br />") : text;
		}

		private void PostprocessTemplate(ITemplate tpl)
		{
			if (string.IsNullOrEmpty(_language) || string.IsNullOrEmpty(_baseLanguage))
				return;

			tpl.Localize(_filePath, _language, _baseLanguage);
		}

		private async Task PostprocessTemplateAsync(ITemplate tpl)
		{
			if (string.IsNullOrEmpty(_language) || string.IsNullOrEmpty(_baseLanguage))
				return;

			await tpl.LocalizeAsync(_filePath, _language, _baseLanguage);
		}
	}
}