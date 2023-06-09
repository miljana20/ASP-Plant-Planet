using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.DTO
{
    public class CartPlantDto
    {
        public PlantDto Plant { get; set; }
        public int Quantity { get; set; }
    }
}
