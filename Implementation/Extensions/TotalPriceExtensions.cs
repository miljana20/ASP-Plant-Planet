using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Extensions
{
    public static class TotalPriceExtensions
    {
        public static decimal GetTotalPrice(int id)
        {
            PlantPlanetContext context = new PlantPlanetContext();
            return context.CartPlants.Include(x => x.Plant).Where(c => c.CartId == id).Sum(x => x.Plant.Price * x.Quantity);
        }
    }
}
