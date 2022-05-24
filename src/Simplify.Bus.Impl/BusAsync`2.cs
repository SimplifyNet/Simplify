using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Simplify.Bus.Process;

namespace Simplify.Bus.Impl;

public class BusAsync<TRequest, TResponse> : IBusAsync<TRequest, TResponse>
{
	private readonly IRequestHandler<TRequest, TResponse> _requestHandler;
	private readonly IReadOnlyList<IBehavior<TRequest, TResponse>>? _behaviors;

	public BusAsync(IRequestHandler<TRequest, TResponse> requestHandler,
		IReadOnlyList<IBehavior<TRequest, TResponse>>? behaviors = null)
	{
		_requestHandler = requestHandler ?? throw new ArgumentNullException(nameof(requestHandler));
		_behaviors = behaviors;
	}

	public Task<TResponse> Send(TRequest request)
	{
		var behavior = _behaviors.WrapUp(_requestHandler);

		return behavior != null
			 ? behavior.Handle(request)
			 : _requestHandler.Handle(request);
	}
}