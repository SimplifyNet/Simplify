namespace Simplify.Bus.Impl.UsabilityTests.Application.Users.Get;

public class GetUserQuery
{
	public GetUserQuery(int userID) => UserID = userID;

	public int UserID { get; }
}