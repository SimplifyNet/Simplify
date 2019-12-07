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
		private Assembly? _assembly;
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

			ValidateTemplateFileExists(filePath);

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

			filePath = FileUtil.ConstructFullFilePath(filePath);

			ValidateTemplateFileExists(filePath);

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

			filePath = FileUtil.ConstructAssemblyFilePath(filePath, assembly);

			ValidateAssemblyTemplateFileExists(filePath, assembly);

			return new TemplateBuilder
			{
				_filePath = filePath,
				_assembly = assembly
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

			var assembly = Assembly.GetCallingAssembly();
			filePath = FileUtil.ConstructAssemblyFilePath(filePath, assembly);

			ValidateAssemblyTemplateFileExists(filePath, assembly);

			return new TemplateBuilder
			{
				_filePath = filePath,
				_assembly = assembly
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

		private static void ValidateTemplateFileExists(string filePath)
		{
			if (!File.Exists(filePath))
				throw new FileNotFoundException($"Template file not found: {filePath}");
		}

		private static void ValidateAssemblyTemplateFileExists(string filePath, Assembly assembly)
		{
			if (FileUtil.AssemblyFileExists(filePath, assembly))
				return;

			throw new FileNotFoundException($"Template file not found: {filePath}");
		}

		private string LoadTemplateText()
		{
			if (_text != null)
				return _text;

			if (_filePath == null)
				throw new InvalidOperationException();

			return _assembly == null
				? FileReader.ReadFile(_filePath)
				: FileReader.ReadFromAssembly(_filePath, _assembly);
		}

		private async Task<string> LoadTemplateTextAsync()
		{
			if (_text != null)
				return _text;

			if (_filePath == null)
				throw new InvalidOperationException();

			return _assembly == null
				? await FileReader.ReadFileAsync(_filePath)
				: await FileReader.ReadFromAssemblyAsync(_filePath, _assembly);
		}

		private string PreprocessTemplateText(string text)
		{
			return _fixLineEndingsHtml ? text.Replace(Environment.NewLine, "<br />") : text;
		}

		private void PostprocessTemplate(ITemplate tpl)
		{
			if (_language == null || _baseLanguage == null)
				return;

			if (_filePath == null)
				return;

			if (_assembly == null)
				tpl.Localize(_filePath, _language, _baseLanguage);
			else
				tpl.LocalizeFromAssembly(_filePath, _assembly, _language, _baseLanguage);
		}

		private async Task PostprocessTemplateAsync(ITemplate tpl)
		{
			if (_language == null || _baseLanguage == null)
				return;

			if (_filePath == null)
				return;

			if (_assembly == null)
				await tpl.LocalizeAsync(_filePath, _language, _baseLanguage);
			else
				await tpl.LocalizeFromAssemblyAsync(_filePath, _assembly, _language, _baseLanguage);
		}
	}
}