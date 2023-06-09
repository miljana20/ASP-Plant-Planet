using Application.UseCases.Commands;
using Application.UseCases.DTO;
using DataAccess;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands
{
    public class EfUpdateUserCommand : EfUseCase, IUpdateUserCommand
    {
        private readonly UpdateUserValidator _validator;
        public EfUpdateUserCommand(PlantPlanetContext context, UpdateUserValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 32;

        public string Name => "Update user";

        public string Description => "";

        public void Execute(UpdateUserDto request)
        {
            _validator.ValidateAndThrow(request);
            var user = Context.Users.Find(request.Id);
            if (!string.IsNullOrEmpty(request.Username))
            {
                user.Username = request.Username;
            }
            if (!string.IsNullOrEmpty(request.Email))
            {
                user.Username = request.Username;
            }
            if (!string.IsNullOrEmpty(request.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }
            user.ModifiedAt = DateTime.UtcNow;
            Context.SaveChanges();
        }
    }
}
