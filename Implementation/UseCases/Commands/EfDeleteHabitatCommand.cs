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
    public class EfDeleteHabitatCommand : EfUseCase, IDeleteHabitatCommand
    {
        public EfDeleteHabitatCommand(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 16;

        public string Name => "Delete habitata";

        public string Description => "";

        public void Execute(int id)
        {
            Habitat habitat = Context.Habitats.Include(x => x.HabitatPlants).Where(x => x.Id == id).FirstOrDefault();

            habitat.DeletedAt = DateTime.UtcNow;
            habitat.IsActive = false;

            foreach (var p in habitat.HabitatPlants)
            {
                p.IsActive = false;
                p.DeletedAt = DateTime.UtcNow;
            }

            Context.SaveChanges();
        }
    }
}
