using System.Threading.Tasks;

namespace Simplify.Pipelines.ProductionLine
{
	/// <summary>
	/// Represent asynchronous conveyor stage
	/// </summary>
	/// <typeparam name="T">Conveyor item type</typeparam>
	public interface IAsyncConveyorStage<in T>
	{
		/// <summary>
		/// Executes the stage.
		/// </summary>
		/// <param name="item">The conveyor executing tem.</param>
		/// <returns></returns>
		Task Execute(T item);
	}
}