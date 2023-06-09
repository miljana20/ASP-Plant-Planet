using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.DTO
{
    public class UserDto : EntityDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
