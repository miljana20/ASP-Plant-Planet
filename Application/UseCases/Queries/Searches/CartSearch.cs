using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Searches
{
    public class CartSearch : PagedSearch
    {
        public DateTime? Date { get; set; }
        public string User { get; set; } = "";
        public string Status { get; set; } = "";
        public decimal MinTotalPrice { get; set; } = 0m;
        public decimal MaxTotalPrice { get; set; } = 0m;
    }
}
