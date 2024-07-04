using AuthLearn.AuthPolicy;
using AuthLearn.Configuration;
using AuthLearn.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace AuthLearn.Services
{

    public class JWTService : IJWTService {
        private readonly JwtOptions _options;
        public JWTService( IOptions<JwtOptions> options ) {
            _options = options.Value;
        }

        public string GenerateToken( User user ) {
            var claims = new List<Claim>() {
                new Claim(CustomClaims.UserId, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes( _options.ExpiresMinutes ),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey( Encoding.UTF8.GetBytes( _options.Secret ) ),
                    algorithm: SecurityAlgorithms.HmacSha256
                    )
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken( token );
            return tokenValue;
        }
    }
}
