namespace Simplify.AutoMapper.Tests.TestData
{
	internal record FoodDto : IFoodDto
	{
		public int Count { get; init; }
		public string? Name { get; init; }
		public string? Source { get; init; }
		public string? Type { get; init; }
	}
}