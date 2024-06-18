using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Futures.Staff.Commands.Create
{
    public  class CreateStaffCommandValidation:AbstractValidator<CreateStaffCommandRequest>
    {
        public CreateStaffCommandValidation()
        {
            RuleFor(c => c.Username).NotEmpty().MinimumLength(5);
            RuleFor(c=>c.Fullname).NotEmpty().MinimumLength(5);
            RuleFor(c => c.OrganizationName).NotEmpty();
        }
    }
}
