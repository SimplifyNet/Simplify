using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Simplify.Bus.Process;

namespace Simplify.Bus.Impl;

public class BusAsync<TRequest> : IBusAsync<TRequest>
{
	private readonly IRequestHandler<TRequest> _requestHandler;
	private readonly IReadOnlyList<IBehavior<TRequest>>? _behaviors;

	public BusAsync(IRequestHandler<TRequest> requestHandler, IReadOnlyList<IBehavior<TRequest>>? behaviors = null)
	{
		_requestHandler = requestHandler ?? throw new ArgumentNullException(nameof(requestHandler));
		_behaviors = behaviors;
	}

	public Task Send(TRequest request)
	{
		var behavior = _behaviors.WrapUp(_requestHandler);

		return behavior != null
			 ? behavior.Handle(request)
			 : _requestHandler.Handle(request);
	}
}