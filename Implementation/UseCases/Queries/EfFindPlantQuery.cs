using Application.Exceptions;
using Application.UseCases.DTO;
using Application.UseCases.Queries;
using Application.UseCases.Queries.Searches;
using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfFindPlantQuery : EfUseCase, IFindPlantQuery
    {
        public EfFindPlantQuery(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 12;

        public string Name => "Find plant";

        public string Description => "";

        public PlantDto Execute(int search)
        {
            Plant plant = Context.Plants.Include(x => x.HabitatPlants)
                                        .ThenInclude(x => x.Habitat)
                                        .Include(x => x.Durability)
                                        .Include(x => x.Species)
                                        .FirstOrDefault(x => x.Id == search && x.IsActive && x.DeletedAt == null);

            if (plant == null)
            {
                throw new EntityNotFoundException(search, nameof(Plant));
            }

            return new PlantDto
            {
                Id = plant.Id,
                Name = plant.Name,
                Price = plant.Price,
                Image = plant.Image,
                Durability = new CategoryDto
                {
                    Id = plant.DurabilityId,
                    Name = plant.Durability.Name
                },
                Species = new CategoryDto
                {
                    Id = plant.SpeciesId,
                    Name = plant.Species.Name
                },
                Habitats = plant.HabitatPlants.Select(x => new CategoryDto
                {
                    Id = x.HabitatId,
                    Name = x.Habitat.Name
                })
            };
        }
    }
}
