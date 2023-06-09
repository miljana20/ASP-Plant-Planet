using Application.Uploads;
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
    public class UpdatePlantValidator : AbstractValidator<UpdatePlantDto>
    {
        public UpdatePlantValidator(PlantPlanetContext context, IBase64FileUploader uploader)
        {
            When(x => x.Name != null, () =>
            {
                RuleFor(x => x.Name).MaximumLength(100)
                                .WithMessage("Maximum length for name is 100")
                                .MinimumLength(2)
                                .WithMessage("Minimum length for name is 2")
                                .Must(x => !context.Plants.Any(u => u.Name == x))
                                .WithMessage("Plant name already in use");
            });

            When(x => x.DurabilityId != null, () =>
            {
                RuleFor(x => x.DurabilityId).Must(x => context.Durabilities.Any(u => u.Id == x))
                                        .WithMessage("Id does not exist in the database");
            });

            When(x => x.SpeciesId != null, () =>
            {
                RuleFor(x => x.SpeciesId).Must(x => context.Species.Any(u => u.Id == x))
                                        .WithMessage("Id does not exist in the database");
            });

            When(x => x.HabitatIds != null, () =>
            {
                RuleForEach(x => x.HabitatIds).ChildRules(id =>
                {
                    id.RuleFor(x => x).Must(x => context.Habitats.Any(u => u.Id == x))
                                      .WithMessage("Id does not exist in the database");
                });
            });

            When(x => x.Price != null, () =>
            {
                RuleFor(x => x.Price).GreaterThan(0)
                                 .WithMessage("Price must be greater than 0");
            });

            When(x => x.Image != null, () =>
            {
                RuleFor(x => x.Image).Must(x => uploader.IsExtensionValid(x) &&
                                                       new List<string> { "jpg", "png", "jpeg" }.Contains(uploader.GetExtension(x)))
                                     .WithMessage("Invalid file extesion. Allowed are .jpg, .png and .jpeg");
            });
        }
    }
}
