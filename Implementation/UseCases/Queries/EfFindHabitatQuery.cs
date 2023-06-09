using Application.Exceptions;
using Application.UseCases.DTO;
using Application.UseCases.Queries;
using Application.UseCases.Queries.Searches;
using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfFindHabitatQuery : EfUseCase, IFindHabitatQuery
    {
        public EfFindHabitatQuery(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 11;

        public string Name => "Find habitat";

        public string Description => "";

        public CategoryDto Execute(int search)
        {
            Habitat habitat = Context.Habitats.FirstOrDefault(x => x.Id == search && x.IsActive && x.DeletedAt == null);

            if (habitat == null)
            {
                throw new EntityNotFoundException(search, nameof(Habitat));
            }

            return new CategoryDto
            {
                Id = habitat.Id,
                Name = habitat.Name,
            };
        }
    }
}
