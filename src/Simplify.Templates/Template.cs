using System;
using System.Collections.Generic;

namespace Simplify.Templates
{
	/// <summary>
	/// Text templates class
	/// </summary>
	public class Template : ITemplate
	{
		private readonly string _textCopy;
		private string _text;
		private IDictionary<string, string?>? _addValues;

		/// <summary>
		/// Initializes a new instance of the <see cref="Template"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <exception cref="ArgumentNullException">text</exception>
		public Template(string text)
		{
			_text = text ?? throw new ArgumentNullException(nameof(text));
			_textCopy = text;
		}

		/// <summary>
		/// Set template variable value (all occurrences will be replaced)
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Value to set</param>
		public ITemplate Set(string variableName, string? value)
		{
			if (variableName == null)
				throw new ArgumentNullException(nameof(variableName));

			ReplaceWithValue(variableName.Trim(), value);

			return this;
		}

		/// <summary>
		/// Add value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Value to set</param>
		public ITemplate Add(string variableName, string? value)
		{
			if (variableName == null)
				throw new ArgumentNullException(nameof(variableName));

			if (_addValues == null)
				_addValues = new Dictionary<string, string?>();

			variableName = variableName.Trim();

			if (!_addValues.ContainsKey(variableName))
				_addValues.Add(variableName, value);
			else
				_addValues[variableName] = _addValues[variableName] + value;

			return this;
		}

		/// <summary>
		/// Get text of the template
		/// </summary>
		public string Get()
		{
			TryReplaceFromAddVariables();

			return _text;
		}

		/// <summary>
		/// Return loaded template to it's initial state
		/// </summary>
		public void RollBack()
		{
			_text = _textCopy;
			_addValues?.Clear();
		}

		/// <summary>
		/// Gets the text of the template and returns loaded template to it's initial state
		/// </summary>
		/// <returns>Text of the template</returns>
		public string GetAndRoll()
		{
			var text = Get();

			RollBack();

			return text;
		}

		private void TryReplaceFromAddVariables()
		{
			if (_addValues == null || _addValues.Count == 0)
				return;

			foreach (var addValue in _addValues)
				ReplaceWithValue(addValue.Key, addValue.Value);

			_addValues.Clear();
		}

		private void ReplaceWithValue(string variableName, string? value)
		{
			var replaceableVariable = "{" + variableName + "}";

			_text = _text.Replace(replaceableVariable, value);
		}

		/*
		/// <summary>
		/// Initialize template class from a string
		/// </summary>
		/// <param name="text">The template text.</param>
		/// <param name="language">Template language.</param>
		/// <param name="fixLineEndingsHtml">If set to <c>true</c> Replace all caret return characters by html <![CDATA[<BR />]]> tag.</param>
		// TODO
		//public Template(string text, string language, bool fixLineEndingsHtml)
		//{
		//throw new NotImplementedException();
		//InitializeText(text, language, fixLineEndingsHtml);
		//_textCopy = _text;
		//}

		/// <summary>
		/// Initialize template class with specified template from a file
		/// </summary>
		/// <param name="filePath">Template file path</param>
		/// <param name="language">Template language (Thread.CurrentThread language will be used by default)</param>
		/// <param name="defaultLanguage">Template default language</param>
		/// <param name="fixLineEndingsHtml">If set to <c>true</c> Replace all caret return characters by html <![CDATA[<BR />]]> tag.</param>
		/// <exception cref="ArgumentNullException">filePath</exception>
		/// <exception cref="TemplateException">Template: file not found:  + filePath</exception>
		// TODO
		//public Template(string filePath, string language, string defaultLanguage, bool fixLineEndingsHtml)
		//{
		//throw new NotImplementedException();
		//if (string.IsNullOrEmpty(filePath))
		//	throw new ArgumentNullException(nameof(filePath));

		//if (!FileSystem.File.Exists(filePath))
		//	throw new TemplateException("Template: file not found: " + filePath);

		//var text = FileSystem.File.ReadAllText(filePath);

		//FilePath = filePath;

		//if (string.IsNullOrEmpty(language))
		//	language = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

		//var currentCultureStringTableFileName = $"{filePath}.{language}.xml";
		//var defaultCultureStringTableFileName = $"{filePath}.{defaultLanguage}.xml";

		//LoadWithLocalization(text,
		//	FileSystem.File.Exists(currentCultureStringTableFileName) ? FileSystem.File.ReadAllText(currentCultureStringTableFileName) : "",
		//	FileSystem.File.Exists(defaultCultureStringTableFileName) ? FileSystem.File.ReadAllText(defaultCultureStringTableFileName) : "",
		//	language, defaultLanguage, fixLineEndingsHtml);
		//}

		/// <summary>
		/// Load template from an assembly resources
		/// </summary>
		/// <param name="workingAssembly">Assembly to load from</param>
		/// <param name="filePath">Template file path</param>
		/// <param name="language">Template language (Thread.CurrentThread language will be used by default)</param>
		/// <param name="defaultLanguage">Template default language</param>
		/// <param name="fixLineEndingsHtml">If set to <c>true</c> Replace all caret return characters by html <![CDATA[<BR />]]> tag.</param>
		/// <exception cref="ArgumentNullException">
		/// workingAssembly
		/// or
		/// filePath
		/// </exception>
		/// <exception cref="TemplateException"></exception>
		// TODO
		//public Template(Assembly workingAssembly, string filePath, string language, string defaultLanguage, bool fixLineEndingsHtml)
		//{
		//throw new NotImplementedException();
		//if (workingAssembly == null)
		//	throw new ArgumentNullException(nameof(workingAssembly));

		//if (filePath == null)
		//	throw new ArgumentNullException(nameof(filePath));

		//FilePath = $"{workingAssembly.GetName().Name}.{filePath}";

		//if (string.IsNullOrEmpty(language))
		//	language = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

		//using (var fileStream = workingAssembly.GetManifestResourceStream(FilePath))
		//{
		//	if (fileStream != null)
		//	{
		//		string text;

		//		using (var sr = new StreamReader(fileStream))
		//			text = sr.ReadToEnd();

		//		var currentCultureStringTableFileName = $"{FilePath}-{language}.xml";
		//		var defaultCultureStringTableFileName = $"{FilePath}-{defaultLanguage}.xml";

		//		using (var currentCultureStStream = workingAssembly.GetManifestResourceStream(currentCultureStringTableFileName))
		//		using (var defaultCultureStStream = workingAssembly.GetManifestResourceStream(defaultCultureStringTableFileName))
		//		{
		//			var currentStringTableText = "";
		//			var defaultStringTableText = "";

		//			if (currentCultureStStream != null)
		//				using (var sr = new StreamReader(currentCultureStStream))
		//					currentStringTableText = sr.ReadToEnd();

		//			if (defaultCultureStStream != null)
		//				using (var sr = new StreamReader(defaultCultureStStream))
		//					defaultStringTableText = sr.ReadToEnd();

		//			LoadWithLocalization(text, currentStringTableText, defaultStringTableText, language, defaultLanguage,
		//				fixLineEndingsHtml);
		//		}
		//	}
		//	else
		//		throw new TemplateException(
		//			$"Template: error loading file from resources in assembly '{workingAssembly.FullName}': {FilePath}");
		//}
		//}

		///// <summary>
		///// Gets the file path of the template file.
		///// </summary>
		///// <value>
		///// The file path of the template file.
		///// </value>
		//public string? FilePath { get; }

		///// <summary>
		///// Template current language
		///// </summary>
		//public string? Language { get; }

		///// <summary>
		///// Template base language
		///// </summary>
		//public string? BaseLanguage { get; }

		//public static TemplateBuilder FromString(string text)
		//{
		//	return new TemplateBuilder();
		//}

		/// <summary>
		/// Initialize template class from a string
		/// </summary>
		/// <param name="text">The template text.</param>
		/// <param name="language">Template language.</param>
		/// <param name="fixLineEndingsHtml">If set to <c>true</c> Replace all caret return characters by html <![CDATA[<BR />]]> tag.</param>
		/// <returns></returns>
		// TODO
		//public static ITemplate FromString(string text, string language = "en", string defaultLanguage = "en", bool fixLineEndingsHtml = false)
		//{
		//	//return new Template(text, language, fixLineEndingsHtml);
		//	throw new NotImplementedException();
		//}

		//public static ITemplate Load(string filePath)
		//{
		//	throw new NotImplementedException();
		//}

		/// <summary>
		/// Load template from a file using calling assembly path prefix in filePath
		/// </summary>
		/// <param name="filePath">Template file path</param>
		/// <param name="language">Template language (Thread.CurrentThread language will be used by default)</param>
		/// <param name="defaultLanguage">Template default language</param>
		/// <param name="fixLineEndingsHtml">If set to <c>true</c> Replace all caret return characters by html <![CDATA[<BR />]]> tag.</param>
		/// <returns></returns>
		// TODO
		//public static ITemplate Load(string filePath, string language, string defaultLanguage = "en", bool fixLineEndingsHtml = false)
		//{
		//	//return new Template($"{Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)}/{filePath}", language, defaultLanguage, fixLineEndingsHtml);
		//	throw new NotImplementedException();
		//}

		/// <summary>
		/// Load template asynchronously from a file using calling assembly path prefix in filePath
		/// </summary>
		/// <param name="filePath">Template file path</param>
		/// <param name="language">Template language (Thread.CurrentThread language will be used by default)</param>
		/// <param name="defaultLanguage">Template default language</param>
		/// <param name="fixLineEndingsHtml">If set to <c>true</c> Replace all caret return characters by html <![CDATA[<BR />]]> tag.</param>
		/// <returns></returns>
		// TODO
		//public static Task<ITemplate> LoadAsync(string filePath, string language, string defaultLanguage = "en", bool fixLineEndingsHtml = false)
		//{
		//	//return Task.Run(() => Load(filePath, language, defaultLanguage, fixLineEndingsHtml));
		//	throw new NotImplementedException();
		//}

		// TODO
		/// <summary>
		/// Load template from an calling assembly resources
		/// </summary>
		/// <param name="filePath">Template file path</param>
		/// <param name="language">Template language (Thread.CurrentThread language will be used by default)</param>
		/// <param name="defaultLanguage">Template default language</param>
		/// <param name="fixLineEndingsHtml">If set to <c>true</c> Replace all caret return characters by html <![CDATA[<BR />]]> tag.</param>
		/// <returns></returns>
		//public static ITemplate FromManifest(string filePath, string language = null, string defaultLanguage = "en", bool fixLineEndingsHtml = false)
		//{
		//	//return new Template(Assembly.GetCallingAssembly(), filePath, language, defaultLanguage, fixLineEndingsHtml);
		//	throw new NotImplementedException();
		//}

		/// <summary>
		/// Load template asynchronously from an calling assembly resources
		/// </summary>
		/// <param name="filePath">Template file path</param>
		/// <param name="language">Template language (Thread.CurrentThread language will be used by default)</param>
		/// <param name="defaultLanguage">Template default language</param>
		/// <param name="fixLineEndingsHtml">If set to <c>true</c> Replace all caret return characters by html <![CDATA[<BR />]]> tag.</param>
		/// <returns></returns>
		// TODO
		//public static Task<ITemplate> FromManifestAsync(string filePath, string language = null, string defaultLanguage = "en", bool fixLineEndingsHtml = false)
		//{
		//	//return Task.Run(() => FromManifest(filePath, language, defaultLanguage, fixLineEndingsHtml));
		//	throw new NotImplementedException();
		//}

		// TODO
		//private void InitializeText(string text, string language = "en", bool fixLineEndingsHtml = false)
		//{
		//	_text = text ?? throw new ArgumentNullException(nameof(text));
		//	Language = language;

		//	if (fixLineEndingsHtml)
		//		_text = _text.Replace(Environment.NewLine, "<br />");
		//}

		// TODO
		//private void LoadWithLocalization(string text,
		//	string currentCultureStringTableText = null,
		//	string defaultCultureStringTableText = null,
		//	string language = "",
		//	string defaultLanguage = "en",
		//	bool fixLineEndingsHtml = false)
		//{
		//	InitializeText(text, fixLineEndingsHtml: fixLineEndingsHtml);

		//	Language = language;

		//	DefaultLanguage = defaultLanguage;

		//	XDocument stringTable;

		//	if (!string.IsNullOrEmpty(currentCultureStringTableText))
		//	{
		//		stringTable = XDocument.Parse(currentCultureStringTableText);

		//		if (stringTable.Root != null)
		//			foreach (var item in stringTable.Root.XPathSelectElements("item").Where(x => x.HasAttributes))
		//				Set((string)item.Attribute("name"),
		//					string.IsNullOrEmpty(item.Value) ? (string)item.Attribute("value") : item.InnerXml().Trim());
		//	}

		//	if (string.IsNullOrEmpty(defaultCultureStringTableText) ||
		//		currentCultureStringTableText == defaultCultureStringTableText)
		//	{
		//		_textCopy = _text;
		//		return;
		//	}

		//	stringTable = XDocument.Parse(defaultCultureStringTableText);

		//	if (stringTable.Root != null)
		//		foreach (var item in stringTable.Root.XPathSelectElements("item").Where(x => x.HasAttributes))
		//			Set((string)item.Attribute("name"),
		//				string.IsNullOrEmpty(item.Value) ? (string)item.Attribute("value") : item.InnerXml().Trim());

		//	_textCopy = _text;
		//}
		*/
	}
}