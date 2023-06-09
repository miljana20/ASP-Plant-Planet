using Application.Exceptions;
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
    public class EfFindCartQuery : EfUseCase, IFindCartQuery
    {
        public EfFindCartQuery(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 9;

        public string Name => "Find cart";

        public string Description => "";

        public CartDto Execute(int search)
        {
            Cart cart = Context.Carts.Include(x => x.CartPlants).ThenInclude(x => x.Plant).ThenInclude(x => x.Durability)
                                     .Include(x => x.CartPlants).ThenInclude(x => x.Plant).ThenInclude(x => x.Species)
                                     .Include(x => x.CartPlants).ThenInclude(x => x.Plant).ThenInclude(x => x.HabitatPlants).ThenInclude(x => x.Habitat)
                                     .Include(x => x.User)
                                     .FirstOrDefault(x => x.Id == search && x.IsActive && x.DeletedAt == null);

            if (cart == null)
            {
                throw new EntityNotFoundException(search, nameof(Cart));
            }

            return new CartDto
            {
                Id = cart.Id,
                User = cart.User.Username,
                Date = cart.Date,
                Status = cart.Status ? "Sent" : "Not sent",
                TotalPrice = TotalPriceExtensions.GetTotalPrice(cart.Id),
                Plants = cart.CartPlants.Select(x => new CartPlantDto
                {
                    Quantity = x.Quantity,
                    Plant = new PlantDto
                    {
                        Id = x.Plant.Id,
                        Name = x.Plant.Name,
                        Durability = new CategoryDto
                        {
                            Id = x.Plant.DurabilityId,
                            Name = x.Plant.Durability.Name
                        },
                        Species = new CategoryDto
                        {
                            Id = x.Plant.SpeciesId,
                            Name = x.Plant.Species.Name
                        },
                        Price = x.Plant.Price,
                        Image = x.Plant.Image,
                        Habitats = x.Plant.HabitatPlants.Select(y => new CategoryDto
                        {
                            Id = y.HabitatId,
                            Name = y.Habitat.Name
                        })
                    }
                })
            };
        }
    }
}
