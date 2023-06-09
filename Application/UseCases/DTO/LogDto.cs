using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.DTO
{
    public class LogDto : EntityDto
    {
        public string Actor { get; set; }
        public string Data { get; set; }
        public string UseCase { get; set; }
        public DateTime Date { get; set; }

    }
}
