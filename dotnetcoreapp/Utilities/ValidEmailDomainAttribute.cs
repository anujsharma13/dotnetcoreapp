using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcoreapp.Utilities
{
    public class ValidEmailDomainAttribute:ValidationAttribute
    {
        private readonly string allowDomain;

        public ValidEmailDomainAttribute(string allowDomain)
        {
            this.allowDomain = allowDomain;
        }
        public override bool IsValid(object value)
        {
            var email = value.ToString().Split('@');
           return email[1].ToUpper()==allowDomain.ToUpper();

        }
    }
}
