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
    public class EfDeleteSpeciesCommand : EfUseCase, IDeleteSpeciesCommand
    {
        public EfDeleteSpeciesCommand(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 17;

        public string Name => "Delete species";

        public string Description => "";

        public void Execute(int id)
        {
            Species species = Context.Species.Include(x => x.Plants).Include(x => x.ChildrenSpecies).Where(x => x.Id == id).FirstOrDefault();

            species.DeletedAt = DateTime.UtcNow;
            species.IsActive = false;

            foreach (var s in species.ChildrenSpecies)
            {
                s.IsActive = false;
                s.DeletedAt = DateTime.UtcNow;
            }

            foreach (var p in species.Plants)
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
