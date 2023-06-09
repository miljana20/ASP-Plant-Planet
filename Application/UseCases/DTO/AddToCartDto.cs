using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.DTO
{
    public class AddToCartDto
    {
        public int CartId { get; set; }
        public int PlantId { get; set; }
        public int Quantity { get; set; }
    }
}
