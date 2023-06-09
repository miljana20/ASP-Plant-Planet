using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.DTO
{
    public class CreateCartDto
    {
        public int UserId { get; set; }
        public IEnumerable<CreateItem> Items { get; set; }
    }

    public class CreateItem
    {
        public int PlantId { get; set; }
        public int Quantity { get; set; }
    }
}
