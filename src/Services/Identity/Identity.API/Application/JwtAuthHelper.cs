using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.API.Application
{
    public static class JwtAuthHelper
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key) =>
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
    }
}
