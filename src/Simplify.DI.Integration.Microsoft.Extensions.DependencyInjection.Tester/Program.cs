using DryIoc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Simplify.DI;
using Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection.Tester;
using Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection.Tester.Setup;
using Simplify.DI.Provider.DryIoc;

var builder = WebApplication.CreateBuilder(args);

// Set up the Simplify.DI (DryIoc) container

DIContainer.Current = new DryIocDIProvider
{
	Container = new Container()
		.With(rules =>
			rules.With(FactoryMethod.ConstructorWithResolvableArguments)
				.WithoutThrowOnRegisteringDisposableTransient())
};

// Plug Simplify.DI into the host so the framework resolves everything through it

builder.Host.UseServiceProviderFactory(new SimplifyDIServiceProviderFactory());

// Registrations using Simplify.DI

DIContainer.Current.RegisterAll();

// Unresolved types fix

DIContainer.Current.Register<IHttpContextAccessor, HttpContextAccessor>(LifetimeType.Singleton);

// Registrations using Microsoft.Extensions.DependencyInjection

builder.Services.RegisterAll();

var app = builder.Build();

if (app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();

app.Run(context => context.Response.WriteAsync("Hello World!"));

await app.RunAsync();
