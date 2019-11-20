using System;
using System.Collections.Generic;

namespace Simplify.Templates
{
	/// <summary>
	/// Text templates class
	/// </summary>
	public class Template : ITemplate
	{
		private string _initialText;
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
			_initialText = text;
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
		/// Sets initial template state equal to current.
		/// </summary>
		public void Commit()
		{
			_initialText = _text;
		}

		/// <summary>
		/// Return template to it's initial state
		/// </summary>
		public void RollBack()
		{
			_text = _initialText;
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
		// TODO
		//public Template(string filePath, string language, string defaultLanguage, bool fixLineEndingsHtml)
		//{
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

		*/
	}
}