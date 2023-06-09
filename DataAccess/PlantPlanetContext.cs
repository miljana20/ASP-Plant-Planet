using DataAccess.Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess
{
    public class PlantPlanetContext : DbContext
    {
        public PlantPlanetContext()
        {

        }
        public PlantPlanetContext(DbContextOptions opt) : base(opt)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartPlantConfiguration());
            modelBuilder.ApplyConfiguration(new DurabilityConfiguration());
            modelBuilder.ApplyConfiguration(new HabitatConfiguration());
            modelBuilder.ApplyConfiguration(new HabitatPlantConfiguration());
            modelBuilder.ApplyConfiguration(new LogConfiguration());
            modelBuilder.ApplyConfiguration(new PlantConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new SpeciesConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());*/
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlantPlanetContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-DQVE25R;Initial Catalog=PalntPlanet;Integrated Security=True");
        }                                  
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Durability> Durabilities { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Habitat> Habitats { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RoleUseCase> RoleUseCases { get; set; }
        public DbSet<HabitatPlant> HabitatPlants { get; set; }
        public DbSet<CartPlant> CartPlants { get; set; }
    }
}
