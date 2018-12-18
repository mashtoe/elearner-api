using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Elearner.API.Helpers {
    public class JwtHelper {
        public int GetUserIdFromToken(HttpRequest request) {
            try {
                var accessTokenFromRequest = request.Headers["Authorization"];
                var bearerToken = accessTokenFromRequest[0];
                var bearerTokenSplit = bearerToken.Split(' ');
                var encodedToken = bearerTokenSplit[1];
                var handler = new JwtSecurityTokenHandler();
                var decodedToken = handler.ReadToken(encodedToken) as JwtSecurityToken;
                var jti = decodedToken.Claims.First(claim => claim.Type == "nameid").Value;
                return int.Parse(jti);
            } catch(Exception ex) {
                return -1;
            }
            
        }
    }
}
