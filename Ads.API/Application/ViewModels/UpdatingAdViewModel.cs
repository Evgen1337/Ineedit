﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ads.API.Application.ViewModels
{
    public class UpdatingAdViewModel
    {
        public int AdId { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public string Comment { get; set; }
    }
}
