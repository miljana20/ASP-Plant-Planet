using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Species : Entity
    {
        public string Name { get; set; }
        public int? ParentSpeciesId { get; set; }

        public Species ParentSpecies { get; set; }

        public virtual ICollection<Plant> Plants { get; set; } = new HashSet<Plant>();
        public virtual ICollection<Species> ChildrenSpecies { get; set; } = new HashSet<Species>();
    }
}
