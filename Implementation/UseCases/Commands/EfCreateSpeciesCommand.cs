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
    public class EfCreateSpeciesCommand : EfUseCase, ICreateSpeciesCommand
    {
        private readonly CreateSpeciesValidator _validator;
        public EfCreateSpeciesCommand(PlantPlanetContext context, CreateSpeciesValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 23;

        public string Name => "Create species";

        public string Description => "";

        public void Execute(CreateSpeciesDto request)
        {
            _validator.ValidateAndThrow(request);
            var species = new Species
            {
                Name = request.Name,
                ParentSpeciesId = request.ParentId
            };
            Context.Species.Add(species);
            Context.SaveChanges();
        }
    }
}
