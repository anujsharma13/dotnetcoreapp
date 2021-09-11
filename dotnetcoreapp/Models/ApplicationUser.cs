using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcoreapp.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string City { get; set; }
    }
}
