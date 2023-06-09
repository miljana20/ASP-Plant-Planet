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
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries
{
    public class EfGetCartQuery : EfUseCase, IGetCartQuery
    {
        public EfGetCartQuery(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 8;

        public string Name => "Search cart";

        public string Description => "";

        public PagedResponseDto<CartDto> Execute(CartSearch search)
        {
            IQueryable<Cart> query = Context.Carts.Include(x => x.CartPlants)
                                                  .Where(x => x.IsActive && x.DeletedAt == null);

            if (!string.IsNullOrEmpty(search.User))
                query = query.Where(x => x.User.Username.ToLower() == search.User.ToLower());

            if (search.Date.HasValue)
                query = query.Where(x => x.Date == search.Date.Value);

            if (!string.IsNullOrEmpty(search.Status))
            {
                if (search.Status == "Not sent")
                    query = query.Where(x => !x.Status);
                if (search.Status == "Sent")
                    query = query.Where(x => x.Status);
            }

            if (search.MaxTotalPrice > 0)
                query = query.Where(x => TotalPriceExtensions.GetTotalPrice(x.Id) < search.MaxTotalPrice);

            if (search.MinTotalPrice > 0)
                query = query.Where(x => TotalPriceExtensions.GetTotalPrice(x.Id) > search.MinTotalPrice);


            return query.ToPagedResponse<Cart, CartDto>(search, x => new CartDto
            {
                Id = x.Id,
                User = x.User.Username,
                Date = x.Date,
                Status = x.Status ? "Sent" : "Not sent",
                TotalPrice = TotalPriceExtensions.GetTotalPrice(x.Id),
                Plants = x.CartPlants.Select(y => new CartPlantDto
                {
                    Quantity = y.Quantity,
                    Plant = new PlantDto
                    {
                        Id = y.Plant.Id,
                        Name = y.Plant.Name,
                        Durability = new CategoryDto
                        {
                            Id = y.Plant.DurabilityId,
                            Name = y.Plant.Durability.Name
                        },
                        Species = new CategoryDto
                        {
                            Id = y.Plant.SpeciesId,
                            Name = y.Plant.Species.Name
                        },
                        Price = y.Plant.Price,
                        Image = y.Plant.Image,
                        Habitats = y.Plant.HabitatPlants.Select(z => new CategoryDto
                        {
                            Id = z.HabitatId,
                            Name = z.Habitat.Name
                        })
                    }
                })
            });
        }
    }
}
