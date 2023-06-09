using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.DTO
{
    public class UpdatePlantDto : UpdateCategoryDto
    {
        public string Image { get; set; }
        public decimal? Price { get; set; }
        public int? DurabilityId { get; set; }
        public int? SpeciesId { get; set; }
        public IEnumerable<int> HabitatIds { get; set; }
    }
}
