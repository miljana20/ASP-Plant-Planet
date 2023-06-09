using Application.UseCases.Commands;
using Application.UseCases.DTO;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands
{
    public class EfUpdateHabitatCommand : EfUseCase, IUpdateHabitatCommand
    {
        private readonly UpdateHabitatValidator _validator;
        public EfUpdateHabitatCommand(PlantPlanetContext context, UpdateHabitatValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 30;

        public string Name => "Update habitat";

        public string Description => "";

        public void Execute(UpdateCategoryDto request)
        {
            _validator.ValidateAndThrow(request);
            var habitat = Context.Habitats.Find(request.Id);
            habitat.Name = request.Name;
            habitat.ModifiedAt = DateTime.UtcNow;
            Context.SaveChanges();
        }
    }
}
