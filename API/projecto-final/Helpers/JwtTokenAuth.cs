using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Projecto_Final.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Projecto_Final.Helpers
{
    public class JwtTokenAuth : IJwtTokenAuth
    {
        private readonly JwtTokenConfig _token;


        public JwtTokenAuth(IOptions<JwtTokenConfig> token) {
            _token = token.Value;
        }

        public string GenerateJwtToken(User user) {

            var Claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
            };

            byte[] key = Encoding.ASCII.GetBytes(_token.Secret);
            
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Issuer = _token.Issuer,
                Audience = _token.Audience,
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.Now.AddMinutes(_token.Expiry),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = handler.CreateToken(descriptor);

            return handler.WriteToken(token);
        }
    }
}
