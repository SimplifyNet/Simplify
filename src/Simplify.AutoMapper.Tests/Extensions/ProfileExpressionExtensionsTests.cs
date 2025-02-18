using AutoMapper;
using NUnit.Framework;
using Simplify.AutoMapper.Extensions;
using Simplify.AutoMapper.Tests.TestData;

namespace Simplify.AutoMapper.Tests.Extensions;

[TestFixture]
public class ProfileExpressionExtensionsTests
{
	#region SetUp

	private readonly FoodSource _source = new() { Category = "Fruit", Name = "Apple", Count = 5 };
	private FoodDto? _dto;
	private IFoodDto? _dtoBase;

	[SetUp]
	public void SetUp()
	{
		_dtoBase = null;
		_dto = null;
	}

	#endregion SetUp

	[Test]
	public void CreateMap_WithDestinationBaseType_ValidMappingCreated()
	{
		// Arrange

		MapperConfiguration cfg = null!;

		// Act & Assert

		Assert.That(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, IFoodDto, FoodDto>()
			.ForMember(d => d.Type, o => o.MapFrom(s => s.Category))
			.ForMember(d => d.Source, o => o.Ignore())), Throws.Nothing);
		Assert.That(() => cfg.AssertConfigurationIsValid(), Throws.Nothing);
	}

	[Test]
	public void MapToType_ToDestinationBaseType_MappedCorrectly()
	{
		// Arrange

		var mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, IFoodDto, FoodDto>()
				.ForMember(d => d.Type, o => o.MapFrom(s => s.Category)))
			.CreateMapper();

		// Act & Assert

		Assert.That(_dtoBase = mapper.Map<IFoodDto>(_source), Is.Not.Null);
		Assert.That(_dtoBase.Type, Is.EqualTo(_source.Category));
		Assert.That(_dtoBase.Name, Is.EqualTo(_source.Name));
		Assert.That(_dtoBase.Count, Is.EqualTo(_source.Count));
	}

	[Test]
	public void MapToType_ToDestinationType_MappedCorrectly()
	{
		// Arrange

		var mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, IFoodDto, FoodDto>()
				.ForMember(d => d.Type, o => o.MapFrom(s => s.Category)))
			.CreateMapper();

		// Act & Assert

		Assert.That(_dto = mapper.Map<FoodDto>(_source), Is.Not.Null);
		Assert.That(_dto.Type, Is.EqualTo(_source.Category));
		Assert.That(_dto.Name, Is.EqualTo(_source.Name));
		Assert.That(_dto.Count, Is.EqualTo(_source.Count));
	}
}