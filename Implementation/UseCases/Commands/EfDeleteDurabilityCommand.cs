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
    public class EfDeleteDurabilityCommand : EfUseCase, IDeleteDurabilityCommand
    {
        public EfDeleteDurabilityCommand(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 18;

        public string Name => "Delete durability";

        public string Description => "";

        public void Execute(int id)
        {
            Durability durability = Context.Durabilities.Include(x => x.Plants).ThenInclude(x => x.CartPlants)
                                                        .Include(x => x.Plants).ThenInclude(x => x.HabitatPlants)
                                                        .Where(x => x.Id == id).FirstOrDefault();

            durability.DeletedAt = DateTime.UtcNow;
            durability.IsActive = false;

            foreach (var p in durability.Plants)
            {
                p.IsActive = false;
                p.DeletedAt = DateTime.UtcNow;

                foreach (var c in p.CartPlants)
                {
                    c.IsActive = false;
                    c.DeletedAt = DateTime.UtcNow;
                }

                foreach (var h in p.HabitatPlants)
                {
                    h.IsActive = false;
                    h.DeletedAt = DateTime.UtcNow;
                }
            }

            Context.SaveChanges();
        }
    }
}
