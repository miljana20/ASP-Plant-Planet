using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Durability : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Plant> Plants { get; set; } = new HashSet<Plant>();
    }
}
