using System;
using System.Security.Claims;
using System.Text;
using ELearner.Core.Entity.BusinessObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace ELearner.Core.Utilities
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _config;

        public SymmetricSecurityKey Key {
            get {
                return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            }
        }

        public TokenGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(UserBO user) {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
                //new Claim("role", "User")
            };

            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}