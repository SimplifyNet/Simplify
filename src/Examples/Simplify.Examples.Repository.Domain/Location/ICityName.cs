using Simplify.Repository;

namespace Simplify.Examples.Repository.Domain.Location;

public interface ICityName : INamedObject
{
	ICity City { get; set; }

	string Language { get; set; }
}