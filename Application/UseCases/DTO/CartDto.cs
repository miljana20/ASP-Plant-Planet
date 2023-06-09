using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.DTO
{
    public class CartDto : EntityDto
    {
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<CartPlantDto> Plants { get; set; }
    }
}
