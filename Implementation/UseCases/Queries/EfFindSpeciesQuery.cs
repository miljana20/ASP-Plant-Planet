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
    public class EfFindSpeciesQuery : EfUseCase, IFindSpeciesQuery
    {
        public EfFindSpeciesQuery(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 14;

        public string Name => "Find species";

        public string Description => "";

        public SpeciesDto Execute(int search)
        {
            Species species = Context.Species.FirstOrDefault(x => x.Id == search && x.IsActive && x.DeletedAt == null);

            if (species == null)
            {
                throw new EntityNotFoundException(search, nameof(Species));
            }

            return new SpeciesDto
            {
                Id = species.Id,
                Name = species.Name,
                ChildSpecies = species.ChildrenSpecies.Select(x => new SpeciesDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
            };
        }
    }
}
