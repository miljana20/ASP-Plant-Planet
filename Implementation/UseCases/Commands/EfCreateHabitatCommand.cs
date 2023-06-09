using Application.UseCases.Commands;
using Application.UseCases.DTO;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands
{
    public class EfCreateHabitatCommand : EfUseCase, ICreateHabitatCommand
    {
        private readonly CreateHabitatValidator _validator;
        public EfCreateHabitatCommand(PlantPlanetContext context, CreateHabitatValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 24;

        public string Name => "Create habitat";

        public string Description => "";

        public void Execute(CreateCategoryDto request)
        {
            _validator.ValidateAndThrow(request);
            var habitat = new Habitat
            {
                Name = request.Name
            };
            Context.Habitats.Add(habitat);
            Context.SaveChanges();
        }
    }
}
