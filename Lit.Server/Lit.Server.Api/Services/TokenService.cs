using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lit.Server.Logic;
using Microsoft.IdentityModel.Tokens;

namespace Lit.Server.Api
{
	public class TokenService : ITokenService
	{
		private readonly SymmetricSecurityKey _key;
		public TokenService(IConfiguration config)
		{
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])); // this reaches into our appsettings
		}
		public string CreateToken(User user)
		{
			var claims = new List<Claim>{
				new Claim(JwtRegisteredClaimNames.NameId,user.Username)
		};
			var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = creds
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

	}
}