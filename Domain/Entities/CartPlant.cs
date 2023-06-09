using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CartPlant : Entity
    {
        public int PlantId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }

        public virtual Plant Plant { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
