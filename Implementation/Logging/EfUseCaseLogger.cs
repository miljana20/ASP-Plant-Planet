using Newtonsoft.Json;
using Application.Logging;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Logging
{
    public class EfUseCaseLogger : IUseCaseLogger
    {
        private readonly PlantPlanetContext _context;

        public EfUseCaseLogger(PlantPlanetContext context)
        {
            _context = context;
        }

        public void Add(UseCaseLogEntry entry)
        {
            _context.Logs.Add(new Domain.Entities.Log
            {
                Actor = entry.Actor,
                ActorId = entry.ActorId,
                UseCaseData = JsonConvert.SerializeObject(entry.Data),
                UseCaseName = entry.UseCaseName,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            });

            _context.SaveChanges();
        }
    }
}
