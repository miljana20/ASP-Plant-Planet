using FluentValidation;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases
{
    public abstract class EfUseCase
    {
        protected PlantPlanetContext Context { get; }

        protected EfUseCase(PlantPlanetContext context)
        {
            Context = context;
        }
    }
}
