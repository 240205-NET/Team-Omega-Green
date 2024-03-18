using Lit.Server.Logic;

namespace Lit.Server.Api
{
	public interface ITokenService
	{
		string CreateToken(User user);
	}
}