using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateBuilderFromFileTests
	{
		private const string LocalTestFilePath = "TestTemplates/Local/TestFile.txt";
		private const string LocalizationTestFilePath = "TestTemplates/Local/LocalizationTest.tpl";

		[SetUp]
		public void Initialize()
		{
		}

		[Test]
		public void Build_FromFile_TemplateGetEqual()
		{
			// Act
			var tpl = TemplateBuilder
				.FromFile(LocalTestFilePath)
				.Build();

			// Assert
			Assert.AreEqual("test", tpl.Get());
		}

		[Test]
		public async Task BuildAsync_FromFile_TemplateGetEqual()
		{
			// Act
			var tpl = await TemplateBuilder
				.FromFile(LocalTestFilePath)
				.BuildAsync();

			// Assert
			Assert.AreEqual("test", tpl.Get());
		}

		[Test]
		public void Build_FromFileLocalizableDifferentFromBase_LocalizableLoadedWithBaseReplacements()
		{
			// Act
			var tpl = TemplateBuilder
				.FromFile(LocalizationTestFilePath)
				.Localizable("ru")
				.Build();

			// Assert
			Assert.AreEqual("текст1 text2", tpl.Get());
		}

		[Test]
		public async Task BuildAsync_FromFileLocalizableDifferentFromBase_LocalizableLoadedWithBaseReplacements()
		{
			// Act
			var tpl = await TemplateBuilder
				.FromFile(LocalizationTestFilePath)
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
				.FromFile(LocalizationTestFilePath)
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
				.FromFile(LocalizationTestFilePath)
				.LocalizableFromCurrentThreadLanguage()
				.BuildAsync();

			// Assert
			Assert.AreEqual("текст1 text2", tpl.Get());
		}
	}
}