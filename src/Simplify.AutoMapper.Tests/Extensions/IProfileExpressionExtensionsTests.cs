using AutoMapper;
using NUnit.Framework;
using Simplify.AutoMapper.Extensions;
using Simplify.AutoMapper.Tests.TestData;

namespace Simplify.AutoMapper.Tests.Extensions
{
	[TestFixture]
	public class IProfileExpressionExtensionsTests
	{
		#region SetUp

		private readonly FoodSource source = new() { Category = "Fruit", Name = "Apple", Count = 5 };
		private FoodDto? dto;
		private IFoodDto? dtoBase;

		[SetUp]
		public void SetUp()
		{
			dtoBase = null;
			dto = null;
		}

		#endregion SetUp

		[Test]
		public void CreateMap_WithDestinationBaseType_ValidMappingCreated()
		{
			// Arrange

			MapperConfiguration cfg = null!;

			// Act & Assert

			Assert.DoesNotThrow(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, IFoodDto, FoodDto>()
					.ForMember(d => d.Type, o => o.MapFrom(s => s.Category))
					.ForMember(d => d.Source, o => o.Ignore())));
			Assert.DoesNotThrow(() => cfg.AssertConfigurationIsValid());
		}

		[Test]
		public void MapToType_ToDestinationBaseType_MappedCorrectly()
		{
			// Arrange

			var mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, IFoodDto, FoodDto>()
					.ForMember(d => d.Type, o => o.MapFrom(s => s.Category)))
				.CreateMapper();

			// Act & Assert

			Assert.NotNull(dtoBase = mapper.Map<IFoodDto>(source));
			Assert.AreEqual(dtoBase.Type, source.Category);
			Assert.AreEqual(dtoBase.Name, source.Name);
			Assert.AreEqual(dtoBase.Count, source.Count);
		}

		[Test]
		public void MapToType_ToDestinationType_MappedCorrectly()
		{
			// Arrange

			var mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, IFoodDto, FoodDto>()
					.ForMember(d => d.Type, o => o.MapFrom(s => s.Category)))
				.CreateMapper();

			// Act & Assert

			Assert.NotNull(dto = mapper.Map<FoodDto>(source));
			Assert.AreEqual(dto.Type, source.Category);
			Assert.AreEqual(dto.Name, source.Name);
			Assert.AreEqual(dto.Count, source.Count);
		}
	}
}