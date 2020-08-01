using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Application.Exceptions
{
    public class InvalidLoginOrPassException : Exception
    {
        public InvalidLoginOrPassException() { }
        public InvalidLoginOrPassException(string message) : base(message) { }
        public InvalidLoginOrPassException(string message, Exception inner) : base(message, inner) { }
    }
}
