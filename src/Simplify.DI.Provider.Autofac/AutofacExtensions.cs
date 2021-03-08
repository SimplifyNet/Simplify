using System;
using Autofac.Core;

namespace Simplify.DI.Provider.Autofac
{
	/// <summary>
	/// Provides Autofac extensions
	/// </summary>
	public static class AutofacExtensions
	{
		private const string ServiceTypePropertyName = "ServiceType";

		/// <summary>
		/// Gets the type of the autofac service from dynamically created field.
		/// </summary>
		/// <param name="service">The service.</param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException">Error reading {ServiceTypePropertyName}</exception>
		public static Type GetAutofacServiceType(this Service service)
		{
			var property = service.GetType().GetProperty(ServiceTypePropertyName) ?? throw new InvalidOperationException($"Error reading {ServiceTypePropertyName}");

			return (Type)property.GetValue(service);
		}
	}
}