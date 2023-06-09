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
    public class EfDeletePlantCommand : EfUseCase, IDeletePlantCommand
    {
        public EfDeletePlantCommand(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 15;

        public string Name => "Delete plant";

        public string Description => "";

        public void Execute(int id)
        {
            Plant plants = Context.Plants.Include(x => x.HabitatPlants).Include(x => x.CartPlants).Where(x => x.Id == id).FirstOrDefault();

            plants.DeletedAt = DateTime.UtcNow;
            plants.IsActive = false;

            foreach (var h in plants.HabitatPlants)
            {
                h.IsActive = false;
                h.DeletedAt = DateTime.UtcNow;
            }

            foreach (var c in plants.CartPlants)
            {
                c.IsActive = false;
                c.DeletedAt = DateTime.UtcNow;
            }

            Context.SaveChanges();
        }
    }
}
