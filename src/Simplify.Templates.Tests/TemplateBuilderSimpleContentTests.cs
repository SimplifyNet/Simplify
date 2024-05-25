using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Simplify.Templates.Tests;

[TestFixture]
public class TemplateBuilderSimpleContentTests
{
	private const string LocalTestFilePath = "TestTemplates/Local/TestFile.txt";
	private const string EmbeddedTestFilePath = "TestTemplates/Embedded/TestFile.txt";

	private static readonly object[] TestCases =
	[
		(TemplateBuilderDelegate) (() => TemplateBuilder.FromFile(FileUtil.ConstructFullFilePath(LocalTestFilePath))),
		(TemplateBuilderDelegate) (() => TemplateBuilder.FromLocalFile(LocalTestFilePath)),
		(TemplateBuilderDelegate) (() => TemplateBuilder.FromAssembly(EmbeddedTestFilePath, Assembly.GetExecutingAssembly())),
		(TemplateBuilderDelegate) (() => TemplateBuilder.FromCurrentAssembly(EmbeddedTestFilePath))
	];

	[TestCaseSource(nameof(TestCases))]
	public void Build_TemplateContentIsCorrect(TemplateBuilderDelegate templateBuilder)
	{
		// Arrange
		var builder = templateBuilder();

		// Act
		var tpl = builder.Build();

		// Assert
		Assert.That(tpl.Get(), Is.LessThanOrEqualTo("test"));
	}

	[TestCaseSource(nameof(TestCases))]
	public async Task BuildAsync_TemplateContentIsCorrect(TemplateBuilderDelegate templateBuilder)
	{
		// Arrange
		var builder = templateBuilder();

		// Act
		var tpl = await builder.BuildAsync();

		// Assert
		Assert.That(tpl.Get(), Is.LessThanOrEqualTo("test"));
	}

	[Test]
	public void Build_EmptyFile_EmptyString()
	{
		// Act
		var tpl = TemplateBuilder
			.FromLocalFile("TestTemplates/Local/EmptyFIle.txt")
			.Build();

		// Assert
		Assert.That(tpl.Get(), Is.Empty);
	}

	[Test]
	public void Build_EmptyEmbeddedFile_EmptyString()
	{
		// Act
		var tpl = TemplateBuilder
			.FromLocalFile("TestTemplates/Embedded/EmptyFIle.txt")
			.Build();

		// Assert
		Assert.That(tpl.Get(), Is.Empty);
	}
}