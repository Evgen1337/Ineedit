using Identity.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Application.ViewModels
{
    public class RegisterViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }
    }
}
