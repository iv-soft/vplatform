using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace IVySoft.VPlatform.WebAPI.Utils
{
    public static class JwtUtils
    {
        public static int GetUserId(ClaimsPrincipal user)
        {
            return int.Parse(user.Claims.Single(
                    y => y.Type == Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti).Value);
        }
    }
}
