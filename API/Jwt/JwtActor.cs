﻿using Application;
using System.Collections.Generic;

namespace API.Jwt
{
    public class JwtActor : IApplicationActor
    {
        public int Id { get; set;}

        public string Email { get; set; }

        public string Username { get; set; }

        public IEnumerable<int> AllowedUseCases { get; set; } 
    }
}
