using Application.UseCases.DTO;
using Application.UseCases.Queries;
using Application.UseCases.Queries.Searches;
using DataAccess;
using Domain.Entities;
using Implementation.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfGetPlantsQuery : EfUseCase, IGetPlantQuery
    {
        public EfGetPlantsQuery(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 2;

        public string Name => "Search plants";

        public string Description => "";

        public PagedResponseDto<PlantDto> Execute(PlantSearch search)
        {
            IQueryable<Plant> query = Context.Plants.Include(x => x.HabitatPlants)
                                                    .Where(x => x.IsActive && x.DeletedAt == null);

            if (!string.IsNullOrEmpty(search.Keyword))
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()) ||
                                         x.HabitatPlants.Any(y => y.Habitat.Name.ToLower().Contains(search.Keyword.ToLower())) ||
                                         x.Species.Name.ToLower().Contains(search.Keyword.ToLower()) ||
                                         x.Species.ParentSpecies.Name.ToLower().Contains(search.Keyword.ToLower()) ||
                                         x.Durability.Name.ToLower().Contains(search.Keyword.ToLower()));

            if (!string.IsNullOrEmpty(search.Name))
                query = query.Where(x => x.Name.ToLower() == search.Name.ToLower());

            if (!string.IsNullOrEmpty(search.Durability))
                query = query.Where(x => x.Durability.Name.ToLower() == search.Durability.ToLower());

            if (!string.IsNullOrEmpty(search.Species))
                query = query.Where(x => x.Species.Name.ToLower() == search.Species.ToLower() ||
                                         x.Species.ParentSpecies.Name.ToLower() == search.Species.ToLower());

            if (!string.IsNullOrEmpty(search.Habitat))
                query = query.Where(x => x.HabitatPlants.Any(y => y.Habitat.Name.ToLower() == search.Habitat.ToLower()));

            if (search.MaxPrice > 0)
                query = query.Where(x => x.Price < search.MaxPrice);

            if (search.MinPrice > 0)
                query = query.Where(x => x.Price > search.MinPrice);


            return query.ToPagedResponse<Plant, PlantDto>(search, x => new PlantDto
            {
                Id = x.Id,
                Name = x.Name,
                Durability = new CategoryDto
                {
                    Id = x.DurabilityId,
                    Name = x.Durability.Name
                },
                Species = new CategoryDto
                {
                    Id = x.SpeciesId,
                    Name = x.Species.Name
                },
                Habitats = x.HabitatPlants.Select(y => new CategoryDto
                {
                    Id = y.HabitatId,
                    Name = y.Habitat.Name
                }),
                Price = x.Price,
                Image = x.Image
            });
        }
    }
}
