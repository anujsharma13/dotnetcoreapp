using dotnetcoreapp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcoreapp.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required]
        [MaxLength(10, ErrorMessage = "less length")]
        [Display(Name = "Name of employee")]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }

        public IFormFile Photo { get; set; }
    }
}
