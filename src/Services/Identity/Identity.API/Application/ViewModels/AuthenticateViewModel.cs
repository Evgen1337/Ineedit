using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Application.ViewModels
{
    public class AuthenticateViewModel
    {
        public AuthenticateViewModel(string userId, string userName, string token)
        {
            UserId = userId;
            Email = userName;
            Token = token;
        }

        public string UserId { get; }

        public string Email { get; }

        public string Token { get; }
    }
}
