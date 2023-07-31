namespace Simplify.Pipelines.ProductionLine;

/// <summary>
/// Represent conveyor stage
/// </summary>
/// <typeparam name="T">Conveyor item type</typeparam>
public interface IConveyorStage<in T>
{
	/// <summary>
	/// Executes the stage.
	/// </summary>
	/// <param name="item">The conveyor executing tem.</param>
	/// <returns></returns>
	void Execute(T item);
}