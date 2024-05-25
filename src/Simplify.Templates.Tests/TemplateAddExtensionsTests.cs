using NUnit.Framework;

namespace Simplify.Templates.Tests;

[TestFixture]
public class TemplateAddExtensionsTests
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
	public void Set_NullTemplate_Empty()
	{
		// Act
		_tpl.Add(VariableName, ((ITemplate)null)!);

		// Assert
		Assert.That(_tpl.Get(), Is.Empty);
	}

	[Test]
	public void Set_Template_SetCorrectly()
	{
		// Act
		_tpl.Add(VariableName, TemplateBuilder.FromString("a").Build());

		// Assert
		Assert.That(_tpl.Get(), Is.EqualTo("a"));
	}

	[Test]
	public void Set_NullObject_Empty()
	{
		// Act
		_tpl.Add(VariableName, (object)null);

		// Assert
		Assert.That(_tpl.Get(), Is.Empty);
	}

	[Test]
	public void Set_Object_ObjectClassName()
	{
		// Act
		_tpl.Add(VariableName, new object());

		// Assert
		Assert.That(_tpl.Get(), Is.EqualTo("System.Object"));
	}

	[Test]
	public void Set_Int_SetCorrectly()
	{
		// Act
		_tpl.Add(VariableName, 17);

		// Assert
		Assert.That(_tpl.Get(), Is.EqualTo("17"));
	}

	[Test]
	public void Set_MultipleInt_AllSetCorrectly()
	{
		// Act

		_tpl.Add(VariableName, 17);
		_tpl.Add(VariableName, 63);

		// Assert
		Assert.That(_tpl.Get(), Is.EqualTo("1763"));
	}

	[Test]
	public void Set_Long_SetCorrectly()
	{
		// Act
		_tpl.Add(VariableName, (long)16);

		// Assert
		Assert.That(_tpl.Get(), Is.EqualTo("16"));
	}

	[Test]
	public void Set_Decimal_SetCorrectly()
	{
		// Act
		_tpl.Add(VariableName, 15.5m);

		// Assert
		Assert.That(_tpl.Get(), Is.EqualTo("15.5"));
	}

	[Test]
	public void Set_Double_SetCorrectly()
	{
		// Act
		_tpl.Add(VariableName, 1123456789123.12);

		// Assert
		Assert.That(_tpl.Get(), Is.EqualTo("1123456789123.12"));
	}
}