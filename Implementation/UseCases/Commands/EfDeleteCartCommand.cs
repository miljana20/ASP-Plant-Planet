using Application.UseCases.Commands;
using Application.UseCases.Queries;
using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands
{
    public class EfDeleteCartCommand : EfUseCase, IDeleteCartCommand
    {
        public EfDeleteCartCommand(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 20;

        public string Name => "Delete cart";

        public string Description => "";

        public void Execute(int id)
        {
            Cart cart = Context.Carts.Include(x => x.CartPlants).Where(x => x.Id == id).FirstOrDefault();

            cart.DeletedAt = DateTime.UtcNow;
            cart.IsActive = false;

            foreach (var c in cart.CartPlants)
            {
                c.IsActive = false;
                c.DeletedAt = DateTime.UtcNow;
            }

            Context.SaveChanges();
        }
    }
}
