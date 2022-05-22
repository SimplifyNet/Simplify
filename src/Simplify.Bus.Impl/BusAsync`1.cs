using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplify.Bus.Impl;

public class BusAsync<TRequest> : IBusAsync<TRequest>
{
	private readonly IRequestHandler<TRequest> _requestHandler;
	private readonly IReadOnlyCollection<IBehavior<TRequest>>? _behaviors;

	public BusAsync(IRequestHandler<TRequest> requestHandler, IReadOnlyCollection<IBehavior<TRequest>>? behaviors = null)
	{
		_requestHandler = requestHandler ?? throw new ArgumentNullException(nameof(requestHandler));
		_behaviors = behaviors;
	}

	public Task Send(TRequest request)
	{
		return _requestHandler.Handle(request);
	}
}