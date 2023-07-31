namespace Simplify.Pipelines.ProductionLine;

/// <summary>
/// Represent conveyor executor
/// </summary>
/// <typeparam name="T">Conveyor item type</typeparam>
public interface IConveyorExecutor<out T>
{
	/// <summary>
	/// Runs the conveyor
	/// </summary>
	/// <returns>Conveyor executing item</returns>
	T Execute();
}