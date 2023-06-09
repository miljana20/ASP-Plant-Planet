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
    public class EfFindRoleQuery : EfUseCase, IFindRoleQuery
    {
        public EfFindRoleQuery(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 13;

        public string Name => "Find role";

        public string Description => "";

        public CategoryDto Execute(int search)
        {
            Role role = Context.Roles.FirstOrDefault(x => x.Id == search && x.IsActive && x.DeletedAt == null);

            if (role == null)
            {
                throw new EntityNotFoundException(search, nameof(Role));
            }

            return new CategoryDto
            {
                Id = role.Id,
                Name = role.Name,
            };
        }
    }
}
