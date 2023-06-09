using Application.Exceptions;
using Application.UseCases.DTO;
using Application.UseCases.Queries;
using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Implementation.UseCases.Queries
{
    public class EfFindUserQuery : EfUseCase, IFindUserQuery
    {
        public EfFindUserQuery(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 7;

        public string Name => "Find user";
        public string Description => "";

        public UserDto Execute(int search)
        {
            User user = Context.Users.Include(x => x.Role)
                                     .FirstOrDefault(x => x.Id == search && x.IsActive && x.DeletedAt == null);

            if (user == null)
            {
                throw new EntityNotFoundException(search, nameof(User));
            }

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                RoleName = user.Role.Name
            };
        }
    }
}
