﻿using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Simplify.Templates.Tests;

[TestFixture]
public class TemplateBuilderLocalizationTests
{
	private const string LocalTestFilePath = "TestTemplates/Local/LocalizationTest.tpl";
	private const string EmbeddedTestFilePath = "TestTemplates/Embedded/LocalizationTest.tpl";

	private static readonly object[] TestCases =
	[
		(TemplateBuilderDelegate) (() => TemplateBuilder.FromFile(FileUtil.ConstructFullFilePath(LocalTestFilePath))),
		(TemplateBuilderDelegate) (() => TemplateBuilder.FromLocalFile(LocalTestFilePath)),
		(TemplateBuilderDelegate) (() => TemplateBuilder.FromAssembly(EmbeddedTestFilePath, Assembly.GetExecutingAssembly())),
		(TemplateBuilderDelegate) (() => TemplateBuilder.FromCurrentAssembly(EmbeddedTestFilePath))
	];

	[TestCaseSource(nameof(TestCases))]
	public void Build_LocalizableDifferentFromBase_LocalizableLoadedWithBaseReplacements(TemplateBuilderDelegate templateBuilder)
	{
		// Arrange
		var builder = templateBuilder();

		// Act
		var tpl = builder
			.Localizable("ru")
			.Build();

		// Assert
		Assert.That(tpl.Get(), Is.EqualTo("текст1 text2"));
	}

	[TestCaseSource(nameof(TestCases))]
	public async Task BuildAsync_LocalizableDifferentFromBase_LocalizableLoadedWithBaseReplacements(TemplateBuilderDelegate templateBuilder)
	{
		// Arrange
		var builder = templateBuilder();

		// Act
		var tpl = await builder
			.Localizable("ru")
			.BuildAsync();

		// Assert
		Assert.That(tpl.Get(), Is.EqualTo("текст1 text2"));
	}

	[TestCaseSource(nameof(TestCases))]
	public void Build_LocalizableFromCurrentThreadLanguageDifferentFromBase_LocalizableLoadedWithBaseReplacements(
		TemplateBuilderDelegate templateBuilder)
	{
		// Arrange

		var builder = templateBuilder();
		Thread.CurrentThread.CurrentCulture = new CultureInfo("ru");

		// Act
		var tpl = builder
			.LocalizableFromCurrentThreadLanguage()
			.Build();

		// Assert
		Assert.That(tpl.Get(), Is.EqualTo("текст1 text2"));
	}

	[TestCaseSource(nameof(TestCases))]
	public async Task BuildAsync_LocalizableFromCurrentThreadLanguageDifferentFromBase_LocalizableLoadedWithBaseReplacements(
		TemplateBuilderDelegate templateBuilder)
	{
		// Arrange

		var builder = templateBuilder();
		Thread.CurrentThread.CurrentCulture = new CultureInfo("ru");

		// Act
		var tpl = await builder
			.LocalizableFromCurrentThreadLanguage()
			.BuildAsync();

		// Assert
		Assert.That(tpl.Get(), Is.EqualTo("текст1 text2"));
	}
}