using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ads.API.Application.Exceptions
{
    public class AdNotFoundException : Exception
    {
        public AdNotFoundException() { }
        public AdNotFoundException(string message) : base(message) { }
        public AdNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
