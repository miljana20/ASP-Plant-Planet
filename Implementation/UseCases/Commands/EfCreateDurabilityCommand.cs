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
    public class EfCreateDurabilityCommand : EfUseCase, ICreateDurabilityCommand
    {
        private readonly CreateDurabilityValidator _validator;
        public EfCreateDurabilityCommand(PlantPlanetContext context, CreateDurabilityValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 25;

        public string Name => "Create durability";

        public string Description => "";

        public void Execute(CreateCategoryDto request)
        {
            _validator.ValidateAndThrow(request);
            var durability = new Durability
            {
                Name = request.Name
            };
            Context.Durabilities.Add(durability);
            Context.SaveChanges();
        }
    }
}
