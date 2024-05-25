using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Simplify.Templates.Tests;

[TestFixture]
public class TemplateBuilderFromStringTests
{
	[Test]
	public void FromString_NullString_TemplateGetEqual()
	{
		// Act
		var ex = Assert.Throws<ArgumentNullException>(() => TemplateBuilder.FromString(null!));

		// Assert
		Assert.That(ex!.Message, Does.Contain("Value cannot be null"));
	}

	[Test]
	public void Build_FromString_TemplateContentIsCorrect()
	{
		// Act
		var tpl = TemplateBuilder
			.FromString("test")
			.Build();

		// Assert
		Assert.That(tpl.Get(), Is.EqualTo("test"));
	}

	[Test]
	public async Task BuildAsync_FromString_TemplateContentIsCorrect()
	{
		// Act
		var tpl = await TemplateBuilder
			.FromString("test")
			.BuildAsync();

		// Assert
		Assert.That(tpl.Get(), Is.EqualTo("test"));
	}

	[Test]
	public void FromStringFixLineEndingsHtml_WithLineBreak_LineEndingReplacedWithBrs()
	{
		// Act
		var tpl = TemplateBuilder
			.FromString($"test{Environment.NewLine}test2")
			.FixLineEndingsHtml()
			.Build();

		// Assert
		Assert.That(tpl.Get(), Is.EqualTo("test<br />test2"));
	}
}