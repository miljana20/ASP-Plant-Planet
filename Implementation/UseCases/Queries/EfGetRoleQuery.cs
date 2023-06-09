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
    public class EfGetRoleQuery : EfUseCase, IGetRoleQuery
    {
        public EfGetRoleQuery(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 6;

        public string Name => "Search roles";

        public string Description => "";

        public PagedResponseDto<CategoryDto> Execute(Search search)
        {
            IQueryable<Role> query = Context.Roles.Where(x => x.IsActive && x.DeletedAt == null); ;

            if (!string.IsNullOrEmpty(search.Keyword))
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));

            if (!string.IsNullOrEmpty(search.Name))
                query = query.Where(x => x.Name.ToLower() == search.Name.ToLower());

            return query.ToPagedResponse<Role, CategoryDto>(search, x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name
            });
        }
    }
}
