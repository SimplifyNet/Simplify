using System.Threading.Tasks;

namespace Simplify.Pipelines.ProductionLine;

/// <summary>
/// Represent asynchronous conveyor executor
/// </summary>
/// <typeparam name="T">Conveyor item type</typeparam>
public interface IAsyncConveyorExecutor<T>
{
	/// <summary>
	/// Runs the conveyor asynchronously
	/// </summary>
	/// <returns>Conveyor executing item</returns>
	Task<T> Execute();
}