using Application.UseCases.DTO;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterValidator(PlantPlanetContext context)
        {
            RuleFor(x => x.Email).NotEmpty()
                                 .WithMessage("Email is required")
                                 .EmailAddress()
                                 .WithMessage("Invalid email format")
                                 .Must(x => !context.Users.Any(u => u.Email == x))
                                 .WithMessage("Email already in use");

            RuleFor(x => x.Username).NotEmpty()
                                    .WithMessage("Username is required")
                                    .Matches("^(?=[a-zA-Z0-9._]{4,20}$)(?!.*[_.]{2})[^_.].*[^_.]$")
                                    .WithMessage("Username invalid format - 4 min, 20 max, letters, numbers and special characters(.,_)")
                                    .Must(x => !context.Users.Any(u => u.Username == x))
                                    .WithMessage("Username is already in use");

            RuleFor(x => x.Password).NotEmpty()
                                    .WithMessage("Password is required")
                                    .Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{4,}$")
                                    .WithMessage("Password invalid format - 4 min letters and numbers only");

        }
    }
}
