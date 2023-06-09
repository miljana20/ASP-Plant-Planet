using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.DTO
{
    public class UpdateSpeciesDto : UpdateCategoryDto
    {
        public int? ParentId { get; set; }
    }
}
