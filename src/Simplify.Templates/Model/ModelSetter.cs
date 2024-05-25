using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Simplify.Templates.Model;

/// <summary>
/// Provides the model setter to template, sets all objects properties to template
/// </summary>
/// <typeparam name="T">The model type</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="ModelSetter{T}" /> class.
/// </remarks>
/// <param name="template">The template.</param>
/// <param name="model">The model.</param>
/// <param name="modelPrefix">The model prefix.</param>
public class ModelSetter<T>(ITemplate template, T model, string? modelPrefix = null) : ModelSetterBase(template, modelPrefix), IModelSetter<T>
	where T : class
{
	private readonly Type _modelType = typeof(T);

	private readonly IList<string> _skipProperties = [];

	/// <summary>
	/// Customizes the specified property data set to template, for example, you can set custom expression to convert DateTime values
	/// </summary>
	/// <typeparam name="TData">The type of the data.</typeparam>
	/// <param name="memberExpression">The member expression.</param>
	/// <param name="dataExpression">The data expression.</param>
	/// <exception cref="System.ArgumentException">memberExpression type is not a MemberExpression</exception>
	public IModelSetter<T> With<TData>(Expression<Func<T, TData>> memberExpression, Func<TData, object> dataExpression)
	{
		var expression = memberExpression.Body as MemberExpression ?? throw new ArgumentException("memberExpression type is not a MemberExpression");

		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (model == null)
			return this;

		_skipProperties.Add(expression.Member.Name);

		var propInfo = _modelType.GetProperty(expression.Member.Name);

		if (propInfo != null)
			Template.Set(FormatModelVariableName(propInfo.Name), dataExpression.Invoke((TData)propInfo.GetValue(model)!));

		return this;
	}

	/// <summary>
	/// Sets the specified object (model) properties into template (replace variables names like Model.MyPropertyName with respective model properties values).
	/// </summary>
	public ITemplate Set()
	{
		var type = typeof(T);

		foreach (var propInfo in type.GetProperties())
		{
			if (_skipProperties.Contains(propInfo.Name)) continue;

			var value = model == null ? null : propInfo.GetValue(model);
			Template.Set(FormatModelVariableName(propInfo.Name), value);
		}

		return Template;
	}

	/// <summary>
	/// Adds the specified object (model) properties into template (adds to variables names like Model.MyPropertyName with respective object (model) properties values), values will be replaced on template Get or GetAndRoll methods call.
	/// </summary>
	public ITemplate Add()
	{
		var type = typeof(T);

		foreach (var propInfo in type.GetProperties())
		{
			if (_skipProperties.Contains(propInfo.Name)) continue;

			var value = model == null ? null : propInfo.GetValue(model);
			Template.Add(FormatModelVariableName(propInfo.Name), value);
		}

		return Template;
	}
}