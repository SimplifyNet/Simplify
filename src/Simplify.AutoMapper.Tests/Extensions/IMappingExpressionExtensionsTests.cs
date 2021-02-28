using System;
using System.Linq.Expressions;
using AutoMapper;
using NUnit.Framework;
using Simplify.AutoMapper.Extensions;
using Simplify.AutoMapper.Tests.TestData;

namespace Simplify.AutoMapper.Tests.Extensions
{
	[TestFixture]
	public class IMappingExpressionExtensionsTests
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

			Assert.NotNull(dto = mapper.Map<FoodDto>(source));
			Assert.AreEqual(dto.Type, source.Category);
		}

		[Test]
		public void MapTo_CorrectDestinationAndSourceExpressions_ValidMappingCreated()
		{
			// Arrange

			MapperConfiguration cfg = null!;

			// Act & Assert

			Assert.DoesNotThrow(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo(d => d.Type, s => s.Category)
					.MapTo(d => d.Source, s => s)));
			Assert.DoesNotThrow(() => cfg.AssertConfigurationIsValid());
			Assert.DoesNotThrow(() => cfg.CreateMapper());
		}

		[Test]
		public void MapTo_CorrectDestinationExpressionAndSourcePath_MappedCorrectly()
		{
			// Arrange

			var mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo(d => d.Type, "Category"))
				.CreateMapper();

			// Act & Assert

			Assert.NotNull(dto = mapper.Map<FoodDto>(source));
			Assert.AreEqual(dto.Type, source.Category);
		}

		[Test]
		public void MapTo_CorrectDestinationExpressionAndSourcePath_ValidMappingCreated()
		{
			// Arrange

			MapperConfiguration cfg = null!;

			// Act & Assert

			Assert.DoesNotThrow(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo(d => d.Type, "Category")
					.MapTo(d => d.Source, s => s)));
			Assert.DoesNotThrow(() => cfg.AssertConfigurationIsValid());
			Assert.DoesNotThrow(() => cfg.CreateMapper());
		}

		[Test]
		public void MapTo_NullDestinationMemberExpression_ThrowsAutoMapperConfigurationException()
		{
			// Arrange

			var nullDestinationExpression = null as Expression<Func<FoodDto, object?>>;

			// Act & Assert

			Assert.Throws<AutoMapperConfigurationException>(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo(nullDestinationExpression!, s => s.Category)));

			Assert.Throws<AutoMapperConfigurationException>(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo(nullDestinationExpression!, "Category")));

			Assert.Throws<AutoMapperConfigurationException>(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo(d => null, s => s.Category)));
		}

		[Test]
		public void MapTo_NullOrEmptyDestinationMemberName_ThrowsAutoMapperConfigurationException()
		{
			// Arrange

			var nullDestinationName = null as string;

			// Act & Assert

			Assert.Throws<AutoMapperConfigurationException>(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo(nullDestinationName!, s => s.Category)));
			Assert.Throws<AutoMapperConfigurationException>(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo("", s => s.Category)));
			Assert.Throws<AutoMapperConfigurationException>(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo("   ", s => s.Category)));

			Assert.Throws<AutoMapperConfigurationException>(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo(nullDestinationName!, "Category")));
			Assert.Throws<AutoMapperConfigurationException>(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo("", "Category")));
			Assert.Throws<AutoMapperConfigurationException>(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo("   ", "Category")));
		}

		[Test]
		public void MapTo_NullOrEmptySourceMemberPath_ThrowsAutoMapperConfigurationException()
		{
			// Arrange

			var nullSourcePath = null as string;

			// Act & Assert

			Assert.Throws<AutoMapperConfigurationException>(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo(d => d.Type, nullSourcePath!)));
			Assert.Throws<AutoMapperConfigurationException>(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo(d => d.Type, "")));
			Assert.Throws<AutoMapperConfigurationException>(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo(d => d.Type, "   ")));

			Assert.Throws<AutoMapperConfigurationException>(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo("Type", nullSourcePath!)));
			Assert.Throws<AutoMapperConfigurationException>(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo("Type", "")));
			Assert.Throws<AutoMapperConfigurationException>(() => new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo("Type", "   ")));
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

			Assert.NotNull(dto = mapper.Map<FoodDto>(source));
			Assert.AreEqual(dto.Source, source.ToString());

			// Arrange

			mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo("Source", nullSourceExpression))
				.CreateMapper();

			// Act & Assert

			Assert.NotNull(dto = mapper.Map<FoodDto>(source));
			Assert.AreEqual(dto.Source, source.ToString());
		}

		[Test]
		public void MapTo_NullSourceMemberExpression_ValidMappingCreated()
		{
			// Arrange

			MapperConfiguration cfg = null!;
			var nullSourceExpression = null as Expression<Func<FoodSource, object?>>;

			// Act & Assert

			Assert.DoesNotThrow(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo(d => d.Type, nullSourceExpression)
					.MapTo(d => d.Source, nullSourceExpression)));
			Assert.DoesNotThrow(() => cfg.AssertConfigurationIsValid());

			Assert.DoesNotThrow(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo("Type", nullSourceExpression)
					.MapTo(d => d.Source, nullSourceExpression)));
			Assert.DoesNotThrow(() => cfg.AssertConfigurationIsValid());
		}

		[Test]
		public void MapTo_NullValueSourceMemberExpression_MappedCorrectly()
		{
			// Arrange

			var mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo(d => d.Type, x => null))
				.CreateMapper();

			// Act & Assert

			Assert.NotNull(dto = mapper.Map<FoodDto>(source));
			Assert.AreEqual(dto.Type, null);

			// Arrange

			mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo("Type", x => null))
				.CreateMapper();

			// Act & Assert

			Assert.NotNull(dto = mapper.Map<FoodDto>(source));
			Assert.AreEqual(dto.Type, null);
		}

		[Test]
		public void MapTo_NullValueSourceMemberExpression_ValidMappingCreated()
		{
			// Arrange

			MapperConfiguration cfg = null!;

			// Act & Assert

			Assert.DoesNotThrow(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo(d => d.Type, s => null)
					.MapTo(d => d.Source, s => null)));
			Assert.DoesNotThrow(() => cfg.AssertConfigurationIsValid());

			Assert.DoesNotThrow(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo("Type", s => null)
					.MapTo("Source", s => null)));
			Assert.DoesNotThrow(() => cfg.AssertConfigurationIsValid());
		}

		[Test]
		public void MapTo_WithoutSourceMemberExpression_MappedCorrectly()
		{
			// Arrange

			var mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo(d => d.Source))
				.CreateMapper();

			// Act & Assert

			Assert.NotNull(dto = mapper.Map<FoodDto>(source));
			Assert.AreEqual(dto.Source, source.ToString());

			// Arrange

			mapper = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo("Source"))
				.CreateMapper();

			// Act & Assert

			Assert.NotNull(dto = mapper.Map<FoodDto>(source));
			Assert.AreEqual(dto.Source, source.ToString());
		}

		[Test]
		public void MapTo_WithoutSourceMemberExpression_ValidMappingCreated()
		{
			// Arrange

			MapperConfiguration cfg = null!;

			// Act & Assert

			Assert.DoesNotThrow(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo(d => d.Type)
					.MapTo(d => d.Source)));
			Assert.DoesNotThrow(() => cfg.AssertConfigurationIsValid());

			Assert.DoesNotThrow(() => cfg = new MapperConfiguration(c => c.CreateMap<FoodSource, FoodDto>()
					.MapTo("Type")
					.MapTo("Source")));
			Assert.DoesNotThrow(() => cfg.AssertConfigurationIsValid());
		}
	}
}