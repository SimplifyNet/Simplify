using System.Threading.Tasks;

namespace Simplify.Pipelines.ProductionLine
{
	/// <summary>
	/// Represent asynchronous conveyor item preparer (retriever) for processing thru conveyor
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IAsyncConveyorItemPreparer<T>
	{
		/// <summary>
		/// Gets the item for conveyor processing.
		/// </summary>
		/// <returns>Conveyor executing item</returns>
		Task<T> GetItem();
	}
}