using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ads.API.Application.Exceptions
{
    public class UserWithAdOwnerDoesntEqualsException : Exception
    {
        public UserWithAdOwnerDoesntEqualsException() { }
        public UserWithAdOwnerDoesntEqualsException(string message) : base(message) { }
        public UserWithAdOwnerDoesntEqualsException(string message, Exception inner) : base(message, inner) { }
    }
}
