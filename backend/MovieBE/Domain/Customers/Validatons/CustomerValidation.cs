using Domain.Customers.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Validatons
{
    public abstract class CustomerValidation<T> : AbstractValidator<T>
    where T : CustomerCommand
    {
        //protected static bool HaveMinimumAge(DateTime birthDate)
        //{
        //    return birthDate <= DateTime.Now.AddYears(-18);
        //}

        protected void ValidateDisplayName()
        {
            RuleFor(c => c.DisplayName)
               .NotEmpty().WithMessage("Please ensure you have entered the DisplayName")
               .Length(2, 100).WithMessage("The DisplayName must have between 2 and 100 characters");
        }

        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
