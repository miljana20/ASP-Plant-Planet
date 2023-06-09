using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Plant : Entity
    {
        public string Name { get; set; }
        public int DurabilityId { get; set; }
        public int SpeciesId { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }

        public Durability Durability { get; set; }
        public Species Species { get; set; }

        public virtual ICollection<HabitatPlant> HabitatPlants { get; set; } = new HashSet<HabitatPlant>();
        public virtual ICollection<CartPlant> CartPlants { get; set; } = new HashSet<CartPlant>();
    }
}
