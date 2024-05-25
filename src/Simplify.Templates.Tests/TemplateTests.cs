using System;
using NUnit.Framework;

namespace Simplify.Templates.Tests;

[TestFixture]
public class TemplateTests
{
	private const string VariableName = "var1";

	private ITemplate _tpl;

	[SetUp]
	public void Initialize()
	{
		// Arrange
		_tpl = TemplateBuilder.FromString("{" + VariableName + "}")
			.Build();
	}

	[Test]
	public void Build_FromNullString_ArgumentNullException()
	{
		// Act
		var ex = Assert.Throws<ArgumentNullException>(() => TemplateBuilder.FromString(null!));

		// Assert
		Assert.That(ex!.Message, Does.Contain("Value cannot be null"));
	}

	[Test]
	public void Set_NullVariableName_ArgumentNullException()
	{
		// Act
		var ex = Assert.Throws<ArgumentNullException>(() => _tpl.Set(null!, "test"));

		// Assert
		Assert.That(ex!.Message, Does.Contain("Value cannot be null"));
	}

	[Test]
	public void Add_NullVariableName_ArgumentNullException()
	{
		// Act
		var ex = Assert.Throws<ArgumentNullException>(() => _tpl.Add(null!, "test"));

		// Assert
		Assert.That(ex!.Message, Does.Contain("Value cannot be null"));
	}

	[Test]
	public void Rollback_RolledAfterChange()
	{
		// Act
		_tpl.Set(VariableName, "test");

		// Assert
		Assert.That(_tpl.Get(), Is.EqualTo("test"));

		// Act
		_tpl.RollBack();

		// Assert
		Assert.That(_tpl.Get(), Is.EqualTo("{var1}"));
	}

	[Test]
	public void GetAndRoll_RolledAfterChangeWithValueReturn()
	{
		// Act
		_tpl.Set(VariableName, "test");

		// Act & Assert
		Assert.That(_tpl.GetAndRoll(), Is.EqualTo("test"));

		// Assert
		Assert.That(_tpl.Get(), Is.EqualTo("{var1}"));
	}
}