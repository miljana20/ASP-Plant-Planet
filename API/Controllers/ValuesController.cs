using DataAccess;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly PlantPlanetContext _context;
        private int[] user = { 1, 2, 3, 4, 5, 7, 10, 11, 12, 14, 20, 22, 26, 28, 34, 35 };
        private int[] admin = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 23, 24, 25, 26, 27, 29, 30, 31, 32, 33 };
        public ValuesController(PlantPlanetContext context)
        {
            _context = context;
        }

        [HttpPost]
        public void Post()
        {
            var habitat = new List<Habitat>
            {
                new Habitat
                {
                    Name ="loose"
                },
                new Habitat
                {
                    Name = "sandy"
                },
                new Habitat
                {
                    Name ="fertilized"
                },
                new Habitat
                {
                    Name ="compacted"
                },
                new Habitat
                {
                    Name ="rocky"
                }
            };
            var species = new List<Species>
            {
                new Species
                {
                    Name ="aloe",
                },
                new Species
                {
                    Name ="cactus"
                },
                new Species
                {
                    Name ="philodendreae"
                },
                new Species
                {
                    Name ="calathea"
                },
                new Species
                {
                    Name ="crassula"
                },
                new Species
                {
                    Name ="succulent"
                }
            };
            var speciesC = new List<Species>
            {
                new Species
                {
                    Name ="aloe vera",
                    ParentSpeciesId = species.First().Id,
                    ParentSpecies = species.First()
                },
                new Species
                {
                    Name ="euphorbia",
                    ParentSpeciesId=species.ElementAt(1).Id,
                    ParentSpecies = species.ElementAt(1)
                },
                new Species
                {
                    Name ="cleistocactus",
                    ParentSpeciesId=species.ElementAt(1).Id,
                    ParentSpecies = species.ElementAt(1)
                }
            };
            species.First().ChildrenSpecies.Add(speciesC.First());
            species.ElementAt(1).ChildrenSpecies.Add(speciesC.ElementAt(1));
            species.ElementAt(1).ChildrenSpecies.Add(speciesC.ElementAt(2));
            var durability = new List<Durability>
            {
                new Durability
                {
                    Name ="annual"
                },
                new Durability
                {
                    Name = "bienial"
                },
                new Durability
                {
                    Name ="perennial"
                }
            };
            var roles = new List<Role>
            {
                new Role
                {
                    Name = "user",
                    IsDefault = true
                },
                new Role
                {
                    Name = "admin",
                    IsDefault = false
                }
            };
            var roleUseCases = new List<RoleUseCase>();
            for(int i=0; i < user.Length; i++)
            {
                roleUseCases.Add(new RoleUseCase
                {
                    RoleId = roles.First().Id,
                    Role = roles.First(),
                    UseCaseId = i+1,
                });
            }
            for (int i = 0; i < admin.Length; i++)
            {
                roleUseCases.Add(new RoleUseCase
                {
                    RoleId = roles.Last().Id,
                    Role = roles.Last(),
                    UseCaseId = i+1
                });
            }
            roles.Last().RoleUseCases.Add(roleUseCases.First());
            var users = new List<User>
            {
                new User
                {
                    Username = "User",
                    Email = "user@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("UserPassword1"),
                    RoleId = roles.First().Id,
                    Role = roles.First()
                },
                new User
                {
                    Username = "Admin",
                    Email = "admin@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("AdminPassword1"),
                    RoleId = roles.Last().Id,
                    Role = roles.Last()
                }
            };
            roles.First().Users.Add(users.First());
            roles.ElementAt(1).Users.Add(users.ElementAt(1));
            var plants = new List<Plant>
            {
                new Plant
                {
                    Name = "aloe blizzard",
                    Image = "aloe-blizzard.jpg",
                    Price = 20m,
                    SpeciesId = species.ElementAt(0).Id,
                    Species = species.ElementAt(0),
                    DurabilityId = durability.ElementAt(2).Id,
                    Durability = durability.ElementAt(2)
                },
                new Plant
                {
                    Name = "aloe juvenna",
                    Image = "aloe-juvenna.jpg",
                    Price = 30m,
                    SpeciesId = species.ElementAt(0).Id,
                    Species = species.ElementAt(0),
                    DurabilityId = durability.ElementAt(2).Id,
                    Durability = durability.ElementAt(2)
                },
                new Plant
                {
                    Name = "aloe vera",
                    Image = "aloe-vera.jpg",
                    Price = 15m,
                    SpeciesId = species.ElementAt(0).Id,
                    Species = species.ElementAt(0),
                    DurabilityId = durability.ElementAt(2).Id,
                    Durability = durability.ElementAt(2)
                },
                new Plant
                {
                    Name = "aralia golden live",
                    Image = "aralia-golden-live.jpg",
                    Price = 40m,
                    SpeciesId = species.ElementAt(2).Id,
                    Species = species.ElementAt(2),
                    DurabilityId = durability.ElementAt(0).Id,
                    Durability = durability.ElementAt(0)
                },
                new Plant
                {
                    Name = "calathea faux",
                    Image = "calathea-faux.jpg",
                    Price = 31m,
                    SpeciesId = species.ElementAt(3).Id,
                    Species = species.ElementAt(3),
                    DurabilityId = durability.ElementAt(0).Id,
                    Durability = durability.ElementAt(0)
                },
                new Plant
                {
                    Name = "calathea rufibarba",
                    Image = "calathea-rufibarba.jpg",
                    Price = 20m,
                    SpeciesId = species.ElementAt(3).Id,
                    Species = species.ElementAt(3),
                    DurabilityId = durability.ElementAt(0).Id,
                    Durability = durability.ElementAt(0)
                },
                new Plant
                {
                    Name = "calathea zebrina",
                    Image = "calathea-zebrina.jpg",
                    Price = 30m,
                    SpeciesId = species.ElementAt(3).Id,
                    Species = species.ElementAt(3),
                    DurabilityId = durability.ElementAt(0).Id,
                    Durability = durability.ElementAt(0)
                },
                new Plant
                {
                    Name = "cereus peruvianus monstrosus",
                    Image = "cereus-peruvianus-monstrosus.jpg",
                    Price = 15m,
                    SpeciesId = species.ElementAt(1).Id,
                    Species = species.ElementAt(1),
                    DurabilityId = durability.ElementAt(1).Id,
                    Durability = durability.ElementAt(1)
                },
                new Plant
                {
                    Name = "cleistocactus brookeae",
                    Image = "cleistocactus-brookeae.jpg",
                    Price = 30m,
                    SpeciesId = species.ElementAt(1).Id,
                    Species = species.ElementAt(1),
                    DurabilityId = durability.ElementAt(2).Id,
                    Durability = durability.ElementAt(2)
                },
                new Plant
                {
                    Name = "crassula campfire",
                    Image = "crassula-campfire.jpg",
                    Price = 40m,
                    SpeciesId = species.ElementAt(4).Id,
                    Species = species.ElementAt(4),
                    DurabilityId = durability.ElementAt(2).Id,
                    Durability = durability.ElementAt(2)
                },
                new Plant
                {
                    Name = "crassula",
                    Image = "crassula.jpg",
                    Price = 20m,
                    SpeciesId = species.ElementAt(4).Id,
                    Species = species.ElementAt(4),
                    DurabilityId = durability.ElementAt(0).Id,
                    Durability = durability.ElementAt(0)
                },
                new Plant
                {
                    Name = "euphorbia trigona",
                    Image = "euphorbia-trigona.jpg",
                    Price = 35m,
                    SpeciesId = species.ElementAt(1).Id,
                    Species = species.ElementAt(1),
                    DurabilityId = durability.ElementAt(1).Id,
                    Durability = durability.ElementAt(1)
                },
                new Plant
                {
                    Name = "calathea guide",
                    Image = "calathea-guide.jpg",
                    Price = 15m,
                    SpeciesId = species.ElementAt(3).Id,
                    Species = species.ElementAt(3),
                    DurabilityId = durability.ElementAt(2).Id,
                    Durability = durability.ElementAt(2)
                },
                new Plant
                {
                    Name = "good luck jade",
                    Image = "good-luck-jade.jpg",
                    Price = 36m,
                    SpeciesId = species.ElementAt(5).Id,
                    Species = species.ElementAt(5),
                    DurabilityId = durability.ElementAt(2).Id,
                    Durability = durability.ElementAt(2)
                },
                new Plant
                {
                    Name = "cactus cleistocactus",
                    Image = "cactus-cleistocactus.jpg",
                    Price = 30m,
                    SpeciesId = speciesC.ElementAt(2).Id,
                    Species = speciesC.ElementAt(2),
                    DurabilityId = durability.ElementAt(2).Id,
                    Durability = durability.ElementAt(2)
                },
                new Plant
                {
                    Name = "euphorbia ales",
                    Image = "euphorbia-ales.jpg",
                    Price = 20m,
                    SpeciesId = speciesC.ElementAt(1).Id,
                    Species = speciesC.ElementAt(1),
                    DurabilityId = durability.ElementAt(1).Id,
                    Durability = durability.ElementAt(1)
                },
                new Plant
                {
                    Name = "aloe vera alba",
                    Image = "aloe-vera-alba.jpg",
                    Price = 36m,
                    SpeciesId = speciesC.ElementAt(1).Id,
                    Species = speciesC.ElementAt(1),
                    DurabilityId = durability.ElementAt(2).Id,
                    Durability = durability.ElementAt(2)
                },
            };
            var habitatPlant = new List<HabitatPlant>
            {
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(0).Id,
                    Plant = plants.ElementAt(0),
                    HabitatId = habitat.ElementAt(1).Id,
                    Habitat = habitat.ElementAt(1)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(0).Id,
                    Plant = plants.ElementAt(0),
                    HabitatId = habitat.ElementAt(0).Id,
                    Habitat = habitat.ElementAt(0)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(1).Id,
                    Plant = plants.ElementAt(1),
                    HabitatId = habitat.ElementAt(0).Id,
                    Habitat = habitat.ElementAt(0)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(2).Id,
                    Plant = plants.ElementAt(2),
                    HabitatId = habitat.ElementAt(3).Id,
                    Habitat = habitat.ElementAt(3)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(2).Id,
                    Plant = plants.ElementAt(2),
                    HabitatId = habitat.ElementAt(2).Id,
                    Habitat = habitat.ElementAt(2)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(3).Id,
                    Plant = plants.ElementAt(3),
                    HabitatId = habitat.ElementAt(4).Id,
                    Habitat = habitat.ElementAt(4)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(3).Id,
                    Plant = plants.ElementAt(3),
                    HabitatId = habitat.ElementAt(3).Id,
                    Habitat = habitat.ElementAt(3)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(4).Id,
                    Plant = plants.ElementAt(4),
                    HabitatId = habitat.ElementAt(0).Id,
                    Habitat = habitat.ElementAt(0)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(5).Id,
                    Plant = plants.ElementAt(5),
                    HabitatId = habitat.ElementAt(0).Id,
                    Habitat = habitat.ElementAt(0)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(6).Id,
                    Plant = plants.ElementAt(6),
                    HabitatId = habitat.ElementAt(4).Id,
                    Habitat = habitat.ElementAt(4)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(6).Id,
                    Plant = plants.ElementAt(6),
                    HabitatId = habitat.ElementAt(1).Id,
                    Habitat = habitat.ElementAt(1)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(6).Id,
                    Plant = plants.ElementAt(6),
                    HabitatId = habitat.ElementAt(0).Id,
                    Habitat = habitat.ElementAt(0)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(7).Id,
                    Plant = plants.ElementAt(7),
                    HabitatId = habitat.ElementAt(4).Id,
                    Habitat = habitat.ElementAt(4)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(8).Id,
                    Plant = plants.ElementAt(8),
                    HabitatId = habitat.ElementAt(0).Id,
                    Habitat = habitat.ElementAt(0)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(8).Id,
                    Plant = plants.ElementAt(8),
                    HabitatId = habitat.ElementAt(4).Id,
                    Habitat = habitat.ElementAt(4)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(9).Id,
                    Plant = plants.ElementAt(9),
                    HabitatId = habitat.ElementAt(4).Id,
                    Habitat = habitat.ElementAt(4)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(10).Id,
                    Plant = plants.ElementAt(10),
                    HabitatId = habitat.ElementAt(3).Id,
                    Habitat = habitat.ElementAt(3)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(11).Id,
                    Plant = plants.ElementAt(11),
                    HabitatId = habitat.ElementAt(2).Id,
                    Habitat = habitat.ElementAt(2)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(11).Id,
                    Plant = plants.ElementAt(11),
                    HabitatId = habitat.ElementAt(4).Id,
                    Habitat = habitat.ElementAt(4)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(12).Id,
                    Plant = plants.ElementAt(12),
                    HabitatId = habitat.ElementAt(1).Id,
                    Habitat = habitat.ElementAt(1)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(13).Id,
                    Plant = plants.ElementAt(13),
                    HabitatId = habitat.ElementAt(4).Id,
                    Habitat = habitat.ElementAt(4)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(14).Id,
                    Plant = plants.ElementAt(14),
                    HabitatId = habitat.ElementAt(0).Id,
                    Habitat = habitat.ElementAt(0)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(14).Id,
                    Plant = plants.ElementAt(14),
                    HabitatId = habitat.ElementAt(2).Id,
                    Habitat = habitat.ElementAt(2)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(14).Id,
                    Plant = plants.ElementAt(14),
                    HabitatId = habitat.ElementAt(3).Id,
                    Habitat = habitat.ElementAt(3)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(16).Id,
                    Plant = plants.ElementAt(16),
                    HabitatId = habitat.ElementAt(3).Id,
                    Habitat = habitat.ElementAt(3)
                },
                new HabitatPlant
                {
                    PlantId = plants.ElementAt(15).Id,
                    Plant = plants.ElementAt(15),
                    HabitatId = habitat.ElementAt(2).Id,
                    Habitat = habitat.ElementAt(2)
                }
            };
            var cart = new List<Cart>
            {
                new Cart
                {
                    UserId = users.First().Id,
                    User = users.First(),
                    Date = DateTime.UtcNow
                },
                new Cart
                {
                    UserId = users.Last().Id,
                    User = users.Last(),
                    Date = DateTime.UtcNow
                },
            };
            var cartPlant = new List<CartPlant>
            {
                new CartPlant
                {
                    CartId = cart.ElementAt(0).Id,
                    Cart = cart.ElementAt(0),
                    Plant = plants.First(),
                    PlantId = plants.First().Id,
                    Quantity = 3
                },
                new CartPlant
                {
                    CartId = cart.ElementAt(0).Id,
                    Cart = cart.ElementAt(0),
                    Plant = plants.ElementAt(7),
                    PlantId = plants.ElementAt(7).Id,
                    Quantity = 1
                },
                new CartPlant
                {
                    CartId = cart.ElementAt(0).Id,
                    Cart = cart.ElementAt(0),
                    Plant = plants.ElementAt(3),
                    PlantId = plants.ElementAt(3).Id,
                    Quantity = 5
                },
                new CartPlant
                {
                    CartId = cart.ElementAt(1).Id,
                    Cart = cart.ElementAt(1),
                    Plant = plants.ElementAt(3),
                    PlantId = plants.ElementAt(3).Id,
                    Quantity = 8
                },
                new CartPlant
                {
                    CartId = cart.ElementAt(1).Id,
                    Cart = cart.ElementAt(1),
                    Plant = plants.ElementAt(12),
                    PlantId = plants.ElementAt(12).Id,
                    Quantity = 1
                }
            };
            try
            {
                _context.Durabilities.AddRange(durability);
                _context.Species.AddRange(species);
                _context.Species.AddRange(speciesC);
                _context.Habitats.AddRange(habitat);
                _context.Plants.AddRange(plants);
                _context.HabitatPlants.AddRange(habitatPlant);
                _context.Roles.AddRange(roles);
                _context.Users.AddRange(users);
                _context.RoleUseCases.AddRange(roleUseCases);
                _context.Carts.AddRange(cart);
                _context.CartPlants.AddRange(cartPlant);

                _context.SaveChanges();
                StatusCode(201);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                StatusCode(500);
            }
        }
    }
}
