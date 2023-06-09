using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart : Entity
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }

        public User User { get; set; }

        public virtual ICollection<CartPlant> CartPlants { get; set; } = new HashSet<CartPlant>();
    }
}
