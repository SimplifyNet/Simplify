using System;
using NUnit.Framework;
using Simplify.Templates.Model;
using Simplify.Templates.Tests.Model.Models;

namespace Simplify.Templates.Tests.Model;

[TestFixture]
public class TemplateModelExtensionsTests
{
	private ITemplate _template;

	[SetUp]
	public void Initialize()
	{
		_template = TemplateBuilder
			.FromString("{ID} {Name} {EMail} {CreationTime}")
			.Build();
	}

	[Test]
	public void Set_NullModel_ReplacesWithNothing()
	{
		// Act
		_template.Model<TestModel>(null!).Set();

		// Assert
		Assert.That(_template.Get(), Is.EqualTo("   "));
	}

	[Test]
	public void Set_NullModelWithWith_ReplacesWithNothing()
	{
		// Act
		_template.Model<TestModel>(null!).With(x => x.CreationTime, x => x.ToString("dd.MM.yyyy")).Set();

		// Assert
		Assert.That(_template.Get(), Is.EqualTo("   "));
	}

	[Test]
	public void Set_Model_SetCorrectly()
	{
		// Arrange
		var model = new TestModel { CreationTime = new DateTime(2014, 10, 5, 0, 0, 0, DateTimeKind.Local), Name = "Foo", EMail = "Foo@example.com", ID = 5 };

		// Act
		_template.Model(model).With(x => x.CreationTime, x => x.ToString("dd.MM.yyyy")).Set();

		// Assert
		Assert.That(_template.Get(), Is.EqualTo("5 Foo Foo@example.com 05.10.2014"));
	}

	[Test]
	public void Set_EverythingIsNull_SetCorrectly()
	{
		// Arrange

		_template = TemplateBuilder
			.FromString("{ID} {Name} {EMail}")
			.Build();

		var model = new TestModel();

		// Act
		_template.Model(model).Set();

		// Assert
		Assert.That(_template.Get(), Is.EqualTo("  "));
	}

	[Test]
	public void Add_TwoModels_DataCombined()
	{
		// Arrange

		_template = TemplateBuilder
			.FromString("{Name} {EMail}")
			.Build();

		var model = new TestModel { Name = "Test", EMail = "test@test.com" };
		var model2 = new TestModel { Name = "Foo", EMail = "foomail@test.com" };

		// Act

		_template.Model(model).Add();
		_template.Model(model2).Add();

		// Assert
		Assert.That(_template.Get(), Is.EqualTo("TestFoo test@test.comfoomail@test.com"));
	}

	[Test]
	public void Set_ModelWithBaseClass_BaseClassFieldsAlsoSet()
	{
		// Arrange

		_template = TemplateBuilder
			.FromString("{ID} {Name}")
			.Build();

		var model = new ChildTestModel { ID = 3, Name = "Hello!" };

		// Act
		_template.Model(model).Set();

		// Assert
		Assert.That(_template.Get(), Is.EqualTo("3 Hello!"));
	}

	[Test]
	public void Set_ModelWithPrefix_PrefixAdded()
	{
		// Arrange

		_template = TemplateBuilder
			.FromString("{Model.ID}")
			.Build();

		var model = new ChildTestModel { ID = 3 };

		// Act
		_template.Model(model, "Model").Set();

		// Assert
		Assert.That(_template.Get(), Is.EqualTo("3"));
	}
}