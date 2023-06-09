using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Searches
{
    public class LogSearch : PagedSearch
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Actor { get; set; } = "";
        public string UseCase { get; set; } = "";
    }
}
