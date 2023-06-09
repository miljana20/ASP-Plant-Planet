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
    public class EfUpdateDurabilityCommand : EfUseCase, IUpdateDurabilityCommand
    {
        private readonly UpdateDurabilityValidator _validator;
        public EfUpdateDurabilityCommand(PlantPlanetContext context, UpdateDurabilityValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 31;

        public string Name => "Update durability";

        public string Description => "";

        public void Execute(UpdateCategoryDto request)
        {
            _validator.ValidateAndThrow(request);
            var durability = Context.Durabilities.Find(request.Id);
            durability.Name = request.Name;
            durability.ModifiedAt = DateTime.UtcNow;
            Context.SaveChanges();
        }
    }
}
