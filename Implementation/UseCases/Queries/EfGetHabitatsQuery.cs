using Application.UseCases.DTO;
using Application.UseCases.Queries;
using Application.UseCases.Queries.Searches;
using DataAccess;
using Domain.Entities;
using Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfGetHabitatsQuery : EfUseCase, IGetHabitatQuery
    {
        public EfGetHabitatsQuery(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 4;

        public string Name => "Search habitats";

        public string Description => "";

        public PagedResponseDto<CategoryDto> Execute(Search search)
        {
            IQueryable<Habitat> query = Context.Habitats.Where(x => x.IsActive && x.DeletedAt == null); ;

            if (!string.IsNullOrEmpty(search.Keyword))
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));

            if (!string.IsNullOrEmpty(search.Name))
                query = query.Where(x => x.Name.ToLower() == search.Name.ToLower());

            return query.ToPagedResponse<Habitat, CategoryDto>(search, x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name
            });
        }
    }
}
