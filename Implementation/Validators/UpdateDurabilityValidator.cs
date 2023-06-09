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

    public class UpdateDurabilityValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateDurabilityValidator(PlantPlanetContext context)
        {
            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("Durability name is required")
                                .MaximumLength(100)
                                .WithMessage("Maximum length for name is 100")
                                .MinimumLength(2)
                                .WithMessage("Minimum length for name is 2")
                                .Must(x => !context.Durabilities.Any(u => u.Name == x))
                                .WithMessage("Durability name already in use");
        }
    }
}
