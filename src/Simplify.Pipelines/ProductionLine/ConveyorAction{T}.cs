namespace Simplify.Pipelines.ProductionLine
{
	/// <summary>
	/// Provides conveyor related action delegate
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="item">The item.</param>
	public delegate void ConveyorAction<in T>(T item);
}