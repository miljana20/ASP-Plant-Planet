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
    public class CreatePlantValidator : AbstractValidator<CreatePlantDto>
    {
        public CreatePlantValidator(PlantPlanetContext context, IBase64FileUploader uploader)
        {
            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("Plant name is required")
                                .MaximumLength(100)
                                .WithMessage("Maximum length for name is 100")
                                .MinimumLength(2)
                                .WithMessage("Minimum length for name is 2")
                                .Must(x => !context.Plants.Any(u => u.Name == x))
                                .WithMessage("Plant name already in use");

            RuleFor(x => x.DurabilityId).NotEmpty()
                                        .WithMessage("Durability id is required")
                                        .Must(x => context.Durabilities.Any(u => u.Id == x))
                                        .WithMessage("Id does not exist in the database");

            RuleFor(x => x.SpeciesId).NotEmpty()
                                        .WithMessage("Species id is required")
                                        .Must(x => context.Species.Any(u => u.Id == x))
                                        .WithMessage("Id does not exist in the database");

            RuleFor(x => x.HabitatIds).NotEmpty()
                                      .WithMessage("Habitat id is required");

            RuleForEach(x => x.HabitatIds).ChildRules(id =>
            {
                id.RuleFor(x => x).Must(x => context.Habitats.Any(u => u.Id == x))
                                  .WithMessage("Id does not exist in the database");
            });

            RuleFor(x => x.Price).NotEmpty()
                                 .WithMessage("Price is required")
                                 .GreaterThan(0)
                                 .WithMessage("Price must be greater than 0");

            RuleFor(x => x.Image).NotEmpty()
                                 .WithMessage("Price is required")
                                 .Must(x => uploader.IsExtensionValid(x) &&
                                            new List<string> { "jpg", "png", "jpeg" }.Contains(uploader.GetExtension(x)))
                                 .WithMessage("Invalid file extesion. Allowed are .jpg, .png and .jpeg");
        }
    }
}
