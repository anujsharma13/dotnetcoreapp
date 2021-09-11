using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcoreapp.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = 1,
                Name = "anuj",
                Department = Dept.IT,
                Email = "anuj@gmail.com"
            },
           new Employee
           {
               Id = 2,
               Name = "abhinav",
               Department = Dept.IT,
               Email = "abhinav@gmail.com"
           });
        }
    }
}
