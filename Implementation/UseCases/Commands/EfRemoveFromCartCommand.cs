using Application.UseCases.Commands;
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
    public class EfRemoveFromCartCommand : EfUseCase, IRemoveFromCartCommand
    {
        public EfRemoveFromCartCommand(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 34;

        public string Name => "Remove plant from cart";

        public string Description => "";

        public void Execute(int id)
        {
            CartPlant item = Context.CartPlants.Where(x => x.Id == id).FirstOrDefault();

            item.DeletedAt = DateTime.UtcNow;
            item.IsActive = false;

            Context.SaveChanges();
        }
    }
}
