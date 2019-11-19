using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Simplify.Xml;

namespace Simplify.Templates
{
	/// <summary>
	/// Provides ITemplate fluent builder
	/// </summary>
	public class TemplateBuilder
	{
		private string? _text;
		private string? _filePath;
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

			PostprocessTemplate(tpl);

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
			_language = Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName;
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

		private string LoadTemplateText()
		{
			if (_text != null)
				return _text;

			if (!string.IsNullOrEmpty(_filePath))
				return ReadFile();

			throw new InvalidOperationException();
		}

		private async Task<string> LoadTemplateTextAsync()
		{
			if (_text != null)
				return _text;

			if (!string.IsNullOrEmpty(_filePath))
				return await ReadFileAsync();

			throw new InvalidOperationException();
		}

		private string PreprocessTemplateText(string text)
		{
			return _fixLineEndingsHtml ? text.Replace(Environment.NewLine, "<br />") : text;
		}

		private void PostprocessTemplate(ITemplate tpl)
		{
		}

		private string ReadFile()
		{
			return File.ReadAllText(_filePath);
		}

		private async Task<string> ReadFileAsync()
		{
			using var sr = new StreamReader(_filePath);

			return await sr.ReadToEndAsync();
		}

		private IDictionary<string, string> LoadStringTableFromString(string fileText)
		{
			var stringTable = XDocument.Parse(fileText);

			if (stringTable.Root != null)
				return stringTable.Root.XPathSelectElements("item")
					.Where(x => x.HasAttributes)
					.ToDictionary(item => (string)item.Attribute("name"),
						item => string.IsNullOrEmpty(item.Value) ? (string)item.Attribute("value") : item.InnerXml().Trim());

			return new Dictionary<string, string>();
		}
	}
}