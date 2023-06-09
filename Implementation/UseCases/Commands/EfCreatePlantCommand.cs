using Application.Uploads;
using Application.UseCases.Commands;
using Application.UseCases.DTO;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands
{
    public class EfCreatePlantCommand : EfUseCase, ICreatePlantCommand
    {
        private readonly CreatePlantValidator _validator;
        private readonly IBase64FileUploader _uploader;
        public EfCreatePlantCommand(PlantPlanetContext context, CreatePlantValidator validator,
            IBase64FileUploader uploader) : base(context)
        {
            _validator = validator;
            _uploader = uploader;
        }

        public int Id => 21;

        public string Name => "Create plant";

        public string Description => "";

        public void Execute(CreatePlantDto request)
        {
            _validator.ValidateAndThrow(request);
            var plant = new Plant
            {
                Name = request.Name,
                Price = request.Price,
                DurabilityId = request.DurabilityId,
                SpeciesId = request.SpeciesId,
                Image = _uploader.Upload(request.Image)
        };
            var habitats = new List<HabitatPlant>();
            foreach (var habitat in request.HabitatIds)
            {
                habitats.Add(new HabitatPlant
                {
                    Plant = plant,
                    HabitatId = habitat
                });
            }
            Context.Plants.Add(plant);
            Context.HabitatPlants.AddRange(habitats);
            Context.SaveChanges();
        }
    }
}
