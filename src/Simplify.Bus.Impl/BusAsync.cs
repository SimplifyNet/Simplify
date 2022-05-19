using System.Threading.Tasks;

namespace Simplify.Bus.Impl;

public class BusAsync : IBusAsync
{
	public Task Send<T>(T command)
	{
		throw new System.NotImplementedException();
	}

	public Task<TResponse> Send<T, TResponse>(T command)
	{
		throw new System.NotImplementedException();
	}

	public Task Publish(IEvent @event)
	{
		throw new System.NotImplementedException();
	}
}