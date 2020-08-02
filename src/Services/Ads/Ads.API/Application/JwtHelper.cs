using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ads.API.Application
{
    public static class JwtHelper
    {
        public static readonly string Issuer = "MyAuthServer";

        public static readonly string Audience = "MyAuthClient";

        private readonly static string Key = "mysupersecret_secretkey!123";

        public static readonly TimeSpan LifeTime = new TimeSpan(7, 0, 0, 0);

        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
             new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));

        public static string GenerateJwt(ClaimsIdentity claimsIdentity)
        {
            var dateTimeNow = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    Issuer,
                    Audience,
                    claimsIdentity.Claims,
                    dateTimeNow,
                    dateTimeNow.Add(LifeTime),
                    new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
