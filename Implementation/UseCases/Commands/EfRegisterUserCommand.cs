using Application.UseCases.Commands;
using Application.UseCases.DTO;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands
{
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        private readonly RegisterValidator _validator;
        public EfRegisterUserCommand(PlantPlanetContext context,
                                   RegisterValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 26;

        public string Name => "Register User";

        public string Description => "Register user with validator and file upload";

        public void Execute(RegisterUserDto request)
        {
            _validator.ValidateAndThrow(request);
            Role defaultRole = Context.Roles.FirstOrDefault(x => x.IsDefault);
            if (defaultRole == null)
            {
                throw new InvalidOperationException("Default role doesn't exist");
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User
            {
                Role = defaultRole,
                Email = request.Email,
                Username = request.Username,
                Password = passwordHash,
            };
            Context.Users.Add(user);
            Context.SaveChanges();
        }
    }
}
