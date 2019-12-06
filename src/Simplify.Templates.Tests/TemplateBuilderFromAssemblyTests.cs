using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateBuilderFromAssemblyTests
	{
		private const string LocalTestFilePath = "TestTemplates/Local/TestFile.txt";
		private const string LocalizationTestFilePath = "TestTemplates/Local/LocalizationTest.tpl";

		[Test]
		public void Build_FromCurrentAssemblyLocalizableDifferentFromBase_LocalizableLoadedWithBaseReplacements()
		{
			// Act
			var tpl = TemplateBuilder
				.FromCurrentAssembly(LocalizationTestFilePath)
				.Localizable("ru")
				.Build();

			// Assert
			Assert.AreEqual("текст1 text2", tpl.Get());
		}

		[Test]
		public async Task BuildAsync_FromCurrentAssemblyLocalizableDifferentFromBase_LocalizableLoadedWithBaseReplacements()
		{
			// Act
			var tpl = await TemplateBuilder
				.FromCurrentAssembly(LocalizationTestFilePath)
				.Localizable("ru")
				.BuildAsync();

			// Assert
			Assert.AreEqual("текст1 text2", tpl.Get());
		}

		[Test]
		public void Build_FromCurrentAssemblyLocalizableFromCurrentThreadLanguageDifferentFromBase_LocalizableLoadedWithBaseReplacements()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("ru");

			// Act
			var tpl = TemplateBuilder
				.FromCurrentAssembly(LocalizationTestFilePath)
				.LocalizableFromCurrentThreadLanguage()
				.Build();

			// Assert
			Assert.AreEqual("текст1 text2", tpl.Get());
		}

		[Test]
		public async Task BuildAsync_FromCurrentAssemblyLocalizableFromCurrentThreadLanguageDifferentFromBase_LocalizableLoadedWithBaseReplacements()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("ru");

			// Act
			var tpl = await TemplateBuilder
				.FromCurrentAssembly(LocalizationTestFilePath)
				.LocalizableFromCurrentThreadLanguage()
				.BuildAsync();

			// Assert
			Assert.AreEqual("текст1 text2", tpl.Get());
		}
	}
}