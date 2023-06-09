using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Log : Entity
    {
        public string Actor { get; set; }
        public int ActorId { get; set; }
        public string UseCaseName { get; set; }
        public string UseCaseData { get; set; }
    }
}
