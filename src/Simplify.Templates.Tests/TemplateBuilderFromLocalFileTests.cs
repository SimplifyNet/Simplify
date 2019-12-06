using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateBuilderFromLocalFileTests
	{
		private const string LocalizationTestFilePath = "TestTemplates/Local/LocalizationTest.tpl";

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