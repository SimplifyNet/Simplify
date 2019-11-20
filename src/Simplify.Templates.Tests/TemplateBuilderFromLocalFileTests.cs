using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateBuilderFromLocalFileTests
	{
		private const string LocalTestFilePath = "TestTemplates/Local/TestFile.txt";
		private const string LocalizationTestFilePath = "TestTemplates/Local/LocalizationTest.tpl";

		[Test]
		public void FromLocalFile_NullPath_ArgumentNullException()
		{
			// Act
			var ex = Assert.Throws<ArgumentException>(() => TemplateBuilder.FromLocalFile(null));

			// Assert
			Assert.AreEqual("Value cannot be null or empty.\r\nParameter name: filePath", ex.Message);
		}

		[Test]
		public void FromLocalFile_FileNotExist_FileNotFoundException()
		{
			// Act
			var ex = Assert.Throws<FileNotFoundException>(() => TemplateBuilder.FromLocalFile("NotFoundFile.txt"));

			// Assert

			Assert.That(ex.Message, Does.StartWith("Template file not found"));
			Assert.That(ex.Message, Does.EndWith("NotFoundFile.txt"));
		}

		[Test]
		public void Build_FromLocalFile_TemplateContentIsCorrect()
		{
			// Act
			var tpl = TemplateBuilder
				.FromLocalFile(LocalTestFilePath)
				.Build();

			// Assert
			Assert.AreEqual("test", tpl.Get());
		}

		[Test]
		public async Task BuildAsync_FromLocalFile_TemplateContentIsCorrect()
		{
			// Act
			var tpl = await TemplateBuilder
				.FromLocalFile(LocalTestFilePath)
				.BuildAsync();

			// Assert
			Assert.AreEqual("test", tpl.Get());
		}

		[Test]
		public void Build_FromLocalFileLocalizableDifferentFromBase_LocalizableLoadedWithBaseReplacements()
		{
			// Act
			var tpl = TemplateBuilder
				.FromLocalFile(LocalizationTestFilePath)
				.Localizable("ru")
				.Build();

			// Assert
			Assert.AreEqual("текст1 text2", tpl.Get());
		}

		[Test]
		public async Task BuildAsync_FromLocalFileLocalizableDifferentFromBase_LocalizableLoadedWithBaseReplacements()
		{
			// Act
			var tpl = await TemplateBuilder
				.FromLocalFile(LocalizationTestFilePath)
				.Localizable("ru")
				.BuildAsync();

			// Assert
			Assert.AreEqual("текст1 text2", tpl.Get());
		}

		[Test]
		public void Build_FromFileLocalizableFromCurrentThreadLanguageDifferentFromBase_LocalizableLoadedWithBaseReplacements()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("ru");

			// Act
			var tpl = TemplateBuilder
				.FromLocalFile(LocalizationTestFilePath)
				.LocalizableFromCurrentThreadLanguage()
				.Build();

			// Assert
			Assert.AreEqual("текст1 text2", tpl.Get());
		}

		[Test]
		public async Task BuildAsync_FromFileLocalizableFromCurrentThreadLanguageDifferentFromBase_LocalizableLoadedWithBaseReplacements()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("ru");

			// Act
			var tpl = await TemplateBuilder
				.FromLocalFile(LocalizationTestFilePath)
				.LocalizableFromCurrentThreadLanguage()
				.BuildAsync();

			// Assert
			Assert.AreEqual("текст1 text2", tpl.Get());
		}
	}
}