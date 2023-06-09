using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HabitatPlant : Entity
    {
        public int PlantId { get; set; }
        public int HabitatId { get; set; }

        public virtual Plant Plant { get; set; }
        public virtual Habitat Habitat { get; set; }
    }
}
