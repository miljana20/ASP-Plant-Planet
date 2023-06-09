using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UseCases.DTO;
using DataAccess;
using FluentValidation;

namespace Implementation.Validators
{
    public class AddToCartValidator : AbstractValidator<AddToCartDto>
    {
        public AddToCartValidator(PlantPlanetContext context)
        {
            RuleFor(x => x.PlantId).NotEmpty()
                                    .WithMessage("Username is required")
                                    .Must(x => context.Plants.Any(u => u.Id == x))
                                    .WithMessage("Id does not exist in the database\"");
            RuleFor(x => x.CartId).NotEmpty()
                                    .WithMessage("Username is required")
                                    .Must(x => context.Carts.Any(u => u.Id == x))
                                    .WithMessage("Id does not exist in the database\"");
        }
    }
}
