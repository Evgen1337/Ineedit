using System;
using System.Collections.Generic;
using System.Text;

namespace Ads.Domain.Exceptions
{
    public class AdsDomainException : Exception
    {
        public AdsDomainException() { }

        public AdsDomainException(string message) : 
            base(message) { }

        public AdsDomainException(string message, Exception inner) : 
            base(message, inner) { }
    }
}
