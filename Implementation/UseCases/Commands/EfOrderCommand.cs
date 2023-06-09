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
    public class EfOrderCommand : EfUseCase, IOrderCommand
    {
        private readonly OrderValidator _validator;
        public EfOrderCommand(PlantPlanetContext context, OrderValidator validator) : base(context)
        {
            _validator = validator;
        }
        public int Id => 28;

        public string Name => "Order products";

        public string Description => "";

        public void Execute(OrderDto request)
        {
            _validator.ValidateAndThrow(request);
            var cart = Context.Carts.Find(request.Id);
            cart.Status = request.Order;
            cart.ModifiedAt = DateTime.UtcNow;
            Context.SaveChanges();
        }
    }
}
