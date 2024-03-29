using System;
using DryIoc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection.Tester.Setup;
using Simplify.DI.Provider.DryIoc;

namespace Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection.Tester;

public class Startup
{
	public IServiceProvider ConfigureServices(IServiceCollection services)
	{
		// DryIoc specific workaround

		var container = new DryIocDIProvider
		{
			Container = new Container()
				.With(rules =>
					rules.With(FactoryMethod.ConstructorWithResolvableArguments)
						.WithoutThrowOnRegisteringDisposableTransient())
		};

		DIContainer.Current = container;

		// Registrations using Microsoft.Extensions.DependencyInjection
		services.RegisterAll();

		// Registrations using Simplify.DI
		DIContainer.Current.RegisterAll();

		// Unresolved types fix

		DIContainer.Current.Register<IHttpContextAccessor, HttpContextAccessor>(LifetimeType.Singleton);

		return DIContainer.Current.IntegrateWithMicrosoftDependencyInjectionAndVerify(services);
		// return DIContainer.Current.IntegrateWithMicrosoftDependencyInjection(services);
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
			app.UseDeveloperExceptionPage();

		app.Run(x => x.Response.WriteAsync("Hello World!"));
	}
}