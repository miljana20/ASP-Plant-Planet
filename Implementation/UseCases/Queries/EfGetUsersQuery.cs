using Application.UseCases.DTO;
using Application.UseCases.Queries;
using Application.UseCases.Queries.Searches;
using DataAccess;
using Domain.Entities;
using Implementation.Extensions;
using Implementation.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfGetUsersQuery : EfUseCase, IGetUserQuery
    {
        public EfGetUsersQuery(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 1;

        public string Name => "Search users";

        public string Description => "";

        public PagedResponseDto<UserDto> Execute(Search search)
        {
            IQueryable<User> query = Context.Users.Where(x => x.IsActive && x.DeletedAt == null);

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Username.ToLower().Contains(search.Keyword.ToLower()) || 
                                         x.Email.ToLower().Contains(search.Keyword.ToLower()));
            }

            return query.ToPagedResponse<User, UserDto>(search, x => new UserDto
                {
                
                    Email = x.Email,
                    Id = x.Id,
                    RoleName = x.Role.Name,
                    Username = x.Username
                }
            );
        }
    }
}
