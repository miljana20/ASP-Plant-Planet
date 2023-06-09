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
    public class UpdateSpeciesValidator : AbstractValidator<UpdateSpeciesDto>
    {
        public UpdateSpeciesValidator(PlantPlanetContext context)
        {
            When(x => x.Name != null, () =>
            {
                RuleFor(x => x.Name).MaximumLength(100)
                                .WithMessage("Maximum length for name is 100")
                                .MinimumLength(2)
                                .WithMessage("Minimum length for name is 2")
                                .Must(x => !context.Species.Any(u => u.Name == x))
                                .WithMessage("Habitat name already in use");
            });

            When(x => x.ParentId != null, () =>
            {
                RuleFor(x => x.ParentId).Must(x => context.Species.Any(u => u.Id == x))
                                    .WithMessage("Id does not exist in the database");
            });
        }
    }
}
