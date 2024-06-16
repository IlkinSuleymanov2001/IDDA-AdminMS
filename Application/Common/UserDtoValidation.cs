using FluentValidation;

namespace Application.Controllers
{
    public class UserDtoValidation:AbstractValidator<UserDto>
    {
        public UserDtoValidation()
        {
            RuleFor(c => c.Name).NotEmpty().MinimumLength(7).WithMessage("7 herf olmalidir minmum");

        }
    }
}
