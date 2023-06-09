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
    public class EfUpdateSpeciesCommand : EfUseCase, IUpdateSpeciesCommand
    {
        private readonly UpdateSpeciesValidator _validator;
        public EfUpdateSpeciesCommand(PlantPlanetContext context, UpdateSpeciesValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 29;

        public string Name => "Update species";

        public string Description => "";

        public void Execute(UpdateSpeciesDto request)
        {
            _validator.ValidateAndThrow(request);
            var species = Context.Species.Find(request.Id);
            if (!string.IsNullOrEmpty(request.Name))
            {
                species.Name = request.Name;
            }
            if (request.ParentId != null)
            {
                species.ParentSpeciesId = (int)request.ParentId;
            }
            species.ModifiedAt = DateTime.UtcNow;
            Context.SaveChanges();
        }
    }
}
