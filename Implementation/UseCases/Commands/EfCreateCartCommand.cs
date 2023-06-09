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
    public class EfCreateCartCommand : EfUseCase, ICreateCartCommand
    {
        private readonly CreateCartValidator _validator;
        public EfCreateCartCommand(PlantPlanetContext context, CreateCartValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 22;

        public string Name => "Create cart";

        public string Description => "";

        public void Execute(CreateCartDto request)
        {
            _validator.ValidateAndThrow(request);
            var cart = new Cart
            {
                UserId = request.UserId,
                Date = DateTime.UtcNow
            };
            var items = new List<CartPlant>();
            foreach(var item in request.Items)
            {
                items.Add(new CartPlant
                {
                    CartId = cart.Id,
                    Quantity = item.Quantity,
                    PlantId = item.PlantId
                });
            }
            Context.Carts.Add(cart);
            Context.CartPlants.AddRange(items);
            Context.SaveChanges();
        }
    }
}
