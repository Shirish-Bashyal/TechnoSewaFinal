using System;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TechnoSewaMaui.Helper
{
    public class JWTHelper
    {
        public bool IsTokenValid(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
                return false;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwtToken);

                // Check expiration
                if (token.ValidTo < DateTime.UtcNow)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetRoleFromToken(string jwtToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

                // The role claim might be in different places depending on your server
                var roleClaim = jsonToken
                    ?.Claims
                    .FirstOrDefault(c =>
                        c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                        || c.Type == "role"
                    );

                return roleClaim?.Value ?? "Consumer"; // Default to Consumer if not found
            }
            catch
            {
                return "Consumer"; // Fallback if token is invalid
            }
        }
    }
}
