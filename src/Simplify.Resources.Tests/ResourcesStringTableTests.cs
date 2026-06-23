using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace Simplify.Resources.Tests;

[TestFixture]
public class ResourcesStringTableTests
{
	private ResourcesStringTable _uow;

	[SetUp]
	public void Initialize()
	{
	}

	[Test]
	public void ResourcesStringTableIndexer_ExistingString_Found()
	{
		//Assign

		_uow = new ResourcesStringTable(true, "ProgramResources");

		// Act
		var testString = _uow["TestString"];

		// Assert
		Assert.That(testString, Is.EqualTo("Hello World!"));
	}

	[Test]
	public void ResourcesStringTableIndexer_CustomAssemblyExistingString_Found()
	{
		//Assign

		_uow = new ResourcesStringTable(Assembly.GetAssembly(typeof(ResourcesStringTableTests)), "ProgramResources");

		// Act
		var testString = _uow["TestString"];

		// Assert
		Assert.That(testString, Is.EqualTo("Hello World!"));
	}

	[Test]
	public void ResourcesStringTableIndexer_NoExistingString_Null()
	{
		// Act
		var testString = _uow["TestString2"];

		// Assert
		Assert.That(testString, Is.Null);
	}

	[Test]
	public void ResourcesStringTableGetString_NoExistingString_Null()
	{
		// Arrange
		_uow = new ResourcesStringTable(true, "ProgramResources");

		// Act
		var result = _uow.GetString("NonExistentKey");

		// Assert
		Assert.That(result, Is.Null);
	}

	[Test]
	public void ResourcesStringTableGetRequiredString_ExistingString_ReturnsValue()
	{
		// Arrange
		_uow = new ResourcesStringTable(true, "ProgramResources");

		// Act
		var result = _uow.GetRequiredString("TestString");

		// Assert
		Assert.That(result, Is.EqualTo("Hello World!"));
	}

	[Test]
	public void ResourcesStringTableGetRequiredString_NoExistingString_ThrowsKeyNotFoundException()
	{
		// Arrange
		_uow = new ResourcesStringTable(true, "ProgramResources");

		// Act & Assert
		Assert.Throws<KeyNotFoundException>(() => _uow.GetRequiredString("NonExistentKey"));
	}
}