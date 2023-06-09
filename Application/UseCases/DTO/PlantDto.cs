using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.DTO
{
    public class PlantDto : CategoryDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public CategoryDto Durability { get; set; }
        public CategoryDto Species { get; set; }
        public IEnumerable<CategoryDto> Habitats { get; set; }
    }
}
