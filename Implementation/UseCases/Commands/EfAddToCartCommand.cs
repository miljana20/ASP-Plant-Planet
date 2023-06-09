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
    public class EfAddToCartCommand : EfUseCase, IAddToCartCommand
    {
        private readonly AddToCartValidator _validator;
        public EfAddToCartCommand(PlantPlanetContext context, AddToCartValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 35;

        public string Name => "Add to cart";

        public string Description => "";

        public void Execute(AddToCartDto request)
        {
            _validator.ValidateAndThrow(request);
            var item = new CartPlant
            {
                CartId = request.CartId,
                PlantId = request.PlantId,
                Quantity = request.Quantity
            };
            Context.CartPlants.Add(item);
            Context.SaveChanges();
        }
    }
}
