﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSPersonal.Models
{
    public class AppUser : IdentityUser
    {
        public int Age { get; set; }
    }
}
