using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ads.API.Application.ViewModels
{
    public class AdViewModel
    {
        public int Id { get; set; }

        public Guid OwnerId { get; set; }

        public string Name { get; set; }

        public AdTypeViewModel AdType { get; set; }

        public string Comment { get; set; }
    }

    public class AdTypeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
