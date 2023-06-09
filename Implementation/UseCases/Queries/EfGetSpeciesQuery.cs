using Application.UseCases;
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
    public class EfGetSpeciesQuery : EfUseCase, IGetSpeciesQuery
    {
        public EfGetSpeciesQuery(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 5;

        public string Name => "Search species";

        public string Description => "";

        public PagedResponseDto<SpeciesDto> Execute(Search search)
        {
            IQueryable<Species> query = Context.Species.Where(x => x.IsActive && x.DeletedAt == null);

            if (!string.IsNullOrEmpty(search.Keyword))
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()) ||
                                         x.ParentSpecies.Name.ToLower().Contains(search.Keyword.ToLower()));

            if (!string.IsNullOrEmpty(search.Name))
                query = query.Where(x => x.Name.ToLower() == search.Name.ToLower() || 
                                         x.ParentSpecies.Name.ToLower() == search.Name.ToLower());

            return query.ToPagedResponse<Species, SpeciesDto>(search, x => new SpeciesDto
            {
                Id = x.Id,
                Name = x.Name,
                ChildSpecies = x.ChildrenSpecies.Select(y => new SpeciesDto
                {
                    Id = y.Id,
                    Name = y.Name
                })
            });
        }
    }
}
