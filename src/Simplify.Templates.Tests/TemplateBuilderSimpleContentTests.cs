using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateBuilderSimpleContentTests
	{
		private const string LocalTestFilePath = "TestTemplates/Local/TestFile.txt";
		private const string EmbeddedTestFilePath = "TestTemplates/Embedded/TestFile.txt";

		private static readonly object[] _testCases =
		{
			(TemplateBuilderDelegate) (() => TemplateBuilder.FromFile(FileUtil.ConstructFullFilePath(LocalTestFilePath))),
			(TemplateBuilderDelegate) (() => TemplateBuilder.FromLocalFile(LocalTestFilePath)),
			(TemplateBuilderDelegate) (() => TemplateBuilder.FromAssembly(EmbeddedTestFilePath, Assembly.GetExecutingAssembly())),
			(TemplateBuilderDelegate) (() => TemplateBuilder.FromCurrentAssembly(EmbeddedTestFilePath))
		};

		[TestCaseSource(nameof(_testCases))]
		public void Build_TemplateContentIsCorrect(TemplateBuilderDelegate templateBuilder)
		{
			// Arrange
			var builder = templateBuilder();

			// Act
			var tpl = builder.Build();

			// Assert
			Assert.AreEqual("test", tpl.Get());
		}

		[TestCaseSource(nameof(_testCases))]
		public async Task BuildAsync_TemplateContentIsCorrect(TemplateBuilderDelegate templateBuilder)
		{
			// Arrange
			var builder = templateBuilder();

			// Act
			var tpl = await builder.BuildAsync();

			// Assert
			Assert.AreEqual("test", tpl.Get());
		}
	}
}