using Application.UseCases.Commands;
using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands
{
    public class EfDeleteUserCommand : EfUseCase, IDeleteUserCommand
    {
        public EfDeleteUserCommand(PlantPlanetContext context) : base(context)
        {
        }

        public int Id => 19;

        public string Name => "Delete user";

        public string Description => "";

        public void Execute(int id)
        {
            User user = Context.Users.Include(x => x.Carts).Where(x => x.Id == id).FirstOrDefault();

            user.DeletedAt = DateTime.UtcNow;
            user.IsActive = false;

            foreach (var c in user.Carts)
            {
                c.IsActive = false;
                c.DeletedAt = DateTime.UtcNow;
            }

            Context.SaveChanges();
        }
    }
}
