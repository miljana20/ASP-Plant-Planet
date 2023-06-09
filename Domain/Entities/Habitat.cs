using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Habitat : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<HabitatPlant> HabitatPlants { get; set; } = new HashSet<HabitatPlant>();
    }
}
