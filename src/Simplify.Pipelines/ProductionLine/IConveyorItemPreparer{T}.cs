namespace Simplify.Pipelines.ProductionLine;

/// <summary>
/// Represent conveyor item preparer (retriever) for processing thru conveyor
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IConveyorItemPreparer<out T>
{
	/// <summary>
	/// Gets the item for conveyor processing.
	/// </summary>
	/// <returns>Conveyor executing item</returns>
	T GetItem();
}