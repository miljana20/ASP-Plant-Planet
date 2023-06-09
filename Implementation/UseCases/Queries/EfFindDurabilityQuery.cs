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
    public class EfFindDurabilityQuery : EfUseCase, IFindDurabilityQuery
    {
        public EfFindDurabilityQuery(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 10;

        public string Name => "Find durability";

        public string Description => "";

        public CategoryDto Execute(int search)
        {
            Durability durability = Context.Durabilities.FirstOrDefault(x => x.Id == search && x.IsActive && x.DeletedAt == null);

            if (durability == null)
            {
                throw new EntityNotFoundException(search, nameof(Durability));
            }

            return new CategoryDto
            {
                Id = durability.Id,
                Name = durability.Name,
            };
        }
    }
}
