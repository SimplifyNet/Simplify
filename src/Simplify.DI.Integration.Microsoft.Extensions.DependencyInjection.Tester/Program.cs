using DryIoc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Simplify.DI;
using Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection;
using Simplify.DI.Integration.Microsoft.Extensions.DependencyInjection.Tester.Setup;
using Simplify.DI.Provider.DryIoc;

var builder = WebApplication.CreateBuilder(args);

// DryIoc specific workaround
DIContainer.Current = new DryIocDIProvider
{
	Container = new Container()
		.With(rules =>
			rules.With(FactoryMethod.ConstructorWithResolvableArguments)
				.WithoutThrowOnRegisteringDisposableTransient())
};

// Registrations using Microsoft.Extensions.DependencyInjection
builder.Services.RegisterAll();

// Registrations using Simplify.DI
DIContainer.Current.RegisterAll();

// Unresolved types fix
DIContainer.Current.Register<IHttpContextAccessor, HttpContextAccessor>(LifetimeType.Singleton);

builder.Host.UseServiceProviderFactory(new DIServiceProviderFactory(DIContainer.Current));

var app = builder.Build();

if (app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();

app.Run(x => x.Response.WriteAsync("Hello World!"));

await app.RunAsync();