using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl;

public class BusAsync<TRequest, TResponse> : IBusAsync<TRequest, TResponse>
{
	private readonly IRequestHandler<TRequest, TResponse> _requestHandler;
	private readonly IReadOnlyCollection<IBehavior<TRequest, TResponse>>? _behaviors;

	public BusAsync(IRequestHandler<TRequest, TResponse> requestHandler,
		IReadOnlyCollection<IBehavior<TRequest, TResponse>>? behaviors = null)
	{
		_requestHandler = requestHandler ?? throw new ArgumentNullException(nameof(requestHandler));
		_behaviors = behaviors;
	}

	public Task<TResponse> Send(TRequest request)
	{
		return _requestHandler.Handle(request);
	}
}