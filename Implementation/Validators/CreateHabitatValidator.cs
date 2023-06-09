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
    public class CreateHabitatValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateHabitatValidator(PlantPlanetContext context)
        {
            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("Habitat name is required")
                                .MaximumLength(100)
                                .WithMessage("Maximum length for name is 100")
                                .MinimumLength(2)
                                .WithMessage("Minimum length for name is 2")
                                .Must(x => !context.Habitats.Any(u => u.Name == x))
                                .WithMessage("Habitat name already in use");
        }
    }
}
