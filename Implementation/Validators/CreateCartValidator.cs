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
    public class CreateCartValidator : AbstractValidator<CreateCartDto>
    {
        public CreateCartValidator(PlantPlanetContext context)
        {
            RuleFor(x => x.UserId).NotEmpty()
                                        .WithMessage("User id is required")
                                        .Must(x => context.Users.Any(u => u.Id == x))
                                        .WithMessage("Id does not exist in the database");

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(x => x.PlantId).NotNull()
                                            .WithMessage("Store is required")
                                            .Must(x => context.Plants.Any(s => s.Id == x))
                                            .WithMessage("Store doesn't exist");

                item.RuleFor(x => x.Quantity).NotNull()
                                             .WithMessage("Quantity is required")
                                             .GreaterThan(0)
                                             .WithMessage("Quantity must be greather than 0");
            });
        }
    }
}
