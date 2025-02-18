using System;
using System.Linq.Expressions;
using AutoMapper;
using NUnit.Framework;
using Simplify.AutoMapper.Extensions;
using Simplify.AutoMapper.Tests.TestData;

namespace Simplify.AutoMapper.Tests.Extensions;

[TestFixture]
public class MappingExpressionExtensionsTests
{
	#region SetUp

	private readonly FoodSource source = new() { Category = "Fruit", Name = "Apple", Count = 5 };
	private FoodDto? dto;

	[SetUp]
	public void SetUp()
	{
		dto = null;
	}

	#endregion SetUp
	[Test]
	public void MapTo_CorrectDestinationAndSourceExpressions_MappedCorrectly()
	{
		// Arrange

		var mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
				.MapTo(d => d.Type, s => s.Category))
			.CreateMapper();

		// Act & Assert

		Assert.That(dto = mapper.Map<FoodDto>(source), Is.Not.Null);
		Assert.That(dto.Type, Is.EqualTo(source.Category));
	}

	[Test]
	public void MapTo_CorrectDestinationAndSourceExpressions_ValidMappingCreated()
	{
		// Arrange

		MapperConfiguration cfg = null!;

		// Act & Assert

		Assert.That(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo(d => d.Type, s => s.Category)
			.MapTo(d => d.Source, s => s)), Throws.Nothing);
		Assert.That(() => cfg.AssertConfigurationIsValid(), Throws.Nothing);
		Assert.That(() => cfg.CreateMapper(), Throws.Nothing);
	}

	[Test]
	public void MapTo_CorrectDestinationExpressionAndSourcePath_MappedCorrectly()
	{
		// Arrange

		var mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
				.MapTo(d => d.Type, "Category"))
			.CreateMapper();

		// Act & Assert

		Assert.That(dto = mapper.Map<FoodDto>(source), Is.Not.Null);
		Assert.That(dto.Type, Is.EqualTo(source.Category));
	}

	[Test]
	public void MapTo_CorrectDestinationExpressionAndSourcePath_ValidMappingCreated()
	{
		// Arrange

		MapperConfiguration cfg = null!;

		// Act & Assert

		Assert.That(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo(d => d.Type, "Category")
			.MapTo(d => d.Source, s => s)), Throws.Nothing);
		Assert.That(() => cfg.AssertConfigurationIsValid(), Throws.Nothing);
		Assert.That(() => cfg.CreateMapper(), Throws.Nothing);
	}

	[Test]
	public void MapTo_NullDestinationMemberExpression_ThrowsAutoMapperConfigurationException()
	{
		// Arrange

		var nullDestinationExpression = null as Expression<Func<FoodDto, object?>>;

		// Act & Assert

		Assert.That(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo(nullDestinationExpression!, s => s.Category)), Throws.TypeOf<AutoMapperConfigurationException>());

		Assert.That(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo(nullDestinationExpression!, "Category")), Throws.TypeOf<AutoMapperConfigurationException>());

		Assert.That(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo(d => null, s => s.Category)), Throws.TypeOf<ArgumentException>());
	}

	[Test]
	public void MapTo_NullOrEmptyDestinationMemberName_ThrowsAutoMapperConfigurationException()
	{
		// Arrange

		var nullDestinationName = null as string;

		// Act & Assert

		Assert.That(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo(nullDestinationName!, s => s.Category)), Throws.TypeOf<AutoMapperConfigurationException>());
		Assert.That(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo("", s => s.Category)), Throws.TypeOf<AutoMapperConfigurationException>());
		Assert.That(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo("   ", s => s.Category)), Throws.TypeOf<AutoMapperConfigurationException>());

		Assert.That(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo(nullDestinationName!, "Category")), Throws.TypeOf<AutoMapperConfigurationException>());
		Assert.That(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo("", "Category")), Throws.TypeOf<AutoMapperConfigurationException>());
		Assert.That(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo("   ", "Category")), Throws.TypeOf<AutoMapperConfigurationException>());
	}

	[Test]
	public void MapTo_NullOrEmptySourceMemberPath_ThrowsAutoMapperConfigurationException()
	{
		// Arrange

		var nullSourcePath = null as string;

		// Act & Assert

		Assert.That(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo(d => d.Type, nullSourcePath!)), Throws.TypeOf<AutoMapperConfigurationException>());
		Assert.That(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo(d => d.Type, "")), Throws.TypeOf<AutoMapperConfigurationException>());
		Assert.That(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo(d => d.Type, "   ")), Throws.TypeOf<AutoMapperConfigurationException>());

		Assert.That(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo("Type", nullSourcePath!)), Throws.TypeOf<AutoMapperConfigurationException>());
		Assert.That(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo("Type", "")), Throws.TypeOf<AutoMapperConfigurationException>());
		Assert.That(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo("Type", "   ")), Throws.TypeOf<AutoMapperConfigurationException>());
	}

	[Test]
	public void MapTo_NullSourceMemberExpression_MappedCorrectly()
	{
		// Arrange

		var nullSourceExpression = null as Expression<Func<FoodSource, object?>>;

		var mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
				.MapTo(d => d.Source, nullSourceExpression))
			.CreateMapper();

		// Act & Assert

		Assert.That(dto = mapper.Map<FoodDto>(source), Is.Not.Null);
		Assert.That(dto.Source, Is.EqualTo(source.ToString()));

		// Arrange

		mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
				.MapTo("Source", nullSourceExpression))
			.CreateMapper();

		// Act & Assert

		Assert.That(dto = mapper.Map<FoodDto>(source), Is.Not.Null);
		Assert.That(dto.Source, Is.EqualTo(source.ToString()));
	}

	[Test]
	public void MapTo_NullSourceMemberExpression_ValidMappingCreated()
	{
		// Arrange

		MapperConfiguration cfg = null!;
		var nullSourceExpression = null as Expression<Func<FoodSource, object?>>;

		// Act & Assert

		Assert.That(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo(d => d.Type, nullSourceExpression)
			.MapTo(d => d.Source, nullSourceExpression)), Throws.Nothing);
		Assert.That(() => cfg.AssertConfigurationIsValid(), Throws.Nothing);

		Assert.That(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo("Type", nullSourceExpression)
			.MapTo(d => d.Source, nullSourceExpression)), Throws.Nothing);
		Assert.That(() => cfg.AssertConfigurationIsValid(), Throws.Nothing);
	}

	[Test]
	public void MapTo_NullValueSourceMemberExpression_MappedCorrectly()
	{
		// Arrange

		var mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
				.MapTo(d => d.Type, x => null))
			.CreateMapper();

		// Act & Assert

		Assert.That(dto = mapper.Map<FoodDto>(source), Is.Not.Null);
		Assert.That(dto.Type, Is.Null);

		// Arrange

		mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
				.MapTo("Type", x => null))
			.CreateMapper();

		// Act & Assert

		Assert.That(dto = mapper.Map<FoodDto>(source), Is.Not.Null);
		Assert.That(dto.Type, Is.Null);
	}

	[Test]
	public void MapTo_NullValueSourceMemberExpression_ValidMappingCreated()
	{
		// Arrange

		MapperConfiguration cfg = null!;

		// Act & Assert

		Assert.That(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo(d => d.Type, s => null)
			.MapTo(d => d.Source, s => null)), Throws.Nothing);
		Assert.That(() => cfg.AssertConfigurationIsValid(), Throws.Nothing);

		Assert.That(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo("Type", s => null)
			.MapTo("Source", s => null)), Throws.Nothing);
		Assert.That(() => cfg.AssertConfigurationIsValid(), Throws.Nothing);
	}

	[Test]
	public void MapTo_WithoutSourceMemberExpression_MappedCorrectly()
	{
		// Arrange

		var mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
				.MapTo(d => d.Source))
			.CreateMapper();

		// Act & Assert

		Assert.That(dto = mapper.Map<FoodDto>(source), Is.Not.Null);
		Assert.That(dto.Source, Is.EqualTo(source.ToString()));

		// Arrange

		mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
				.MapTo("Source"))
			.CreateMapper();

		// Act & Assert

		Assert.That(dto = mapper.Map<FoodDto>(source), Is.Not.Null);
		Assert.That(dto.Source, Is.EqualTo(source.ToString()));
	}

	[Test]
	public void MapTo_WithoutSourceMemberExpression_ValidMappingCreated()
	{
		// Arrange

		MapperConfiguration cfg = null!;

		// Act & Assert

		Assert.That(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo(d => d.Type)
			.MapTo(d => d.Source)), Throws.Nothing);
		Assert.That(() => cfg.AssertConfigurationIsValid(), Throws.Nothing);

		Assert.That(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
			.MapTo("Type")
			.MapTo("Source")), Throws.Nothing);
		Assert.That(() => cfg.AssertConfigurationIsValid(), Throws.Nothing);
	}
}