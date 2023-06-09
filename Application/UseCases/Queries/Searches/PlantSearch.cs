using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Searches
{
    public class PlantSearch : Search
    {
        public string Durability { get; set; } = "";
        public string Species { get; set; } = "";
        public string Habitat { get; set; } = "";
        public decimal MaxPrice { get; set; } = 0m;
        public decimal MinPrice { get; set; } = 0m;

    }
}
