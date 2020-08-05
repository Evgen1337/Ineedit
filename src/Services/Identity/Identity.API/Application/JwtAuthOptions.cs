using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API
{
    public class JwtAuthOptions
    {
        [Required]
        public string Issuer { get; set; }

        [Required]
        public string Audience { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public TimeSpan LifeTime { get; set; }
    }
}
