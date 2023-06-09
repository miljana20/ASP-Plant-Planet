using Application.Uploads;
using Application.UseCases.Commands;
using Application.UseCases.DTO;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Implementation.UseCases.Commands
{
    public class EfUpdatePlantCommand : EfUseCase, IUpdatePlantCommand
    {
        private readonly UpdatePlantValidator _validator;
        private readonly IBase64FileUploader _uploader;
        public EfUpdatePlantCommand(PlantPlanetContext context, UpdatePlantValidator validator,
            IBase64FileUploader uploader) : base(context)
        {
            _validator = validator;
            _uploader = uploader;
        }

        public int Id => 27;

        public string Name => "Update plant";

        public string Description => "";

        public void Execute(UpdatePlantDto request)
        {
            _validator.ValidateAndThrow(request);
            var plant = Context.Plants.Find(request.Id);
            if (!string.IsNullOrEmpty(request.Name))
            {
                plant.Name = request.Name;
            }
            if (request.DurabilityId != null)
            {
                plant.DurabilityId = (int)request.DurabilityId;
            }
            if (request.SpeciesId != null)
            {
                plant.SpeciesId = (int)request.SpeciesId;
            }
            if (request.HabitatIds != null)
            {
                var habitats = new List<HabitatPlant>();
                foreach (var habitat in request.HabitatIds)
                {
                    var hab = Context.HabitatPlants.Where(x => x.HabitatId == habitat && x.PlantId == request.Id).FirstOrDefault();
                    if (hab != null)
                    {
                        hab.DeletedAt = DateTime.UtcNow;
                    }
                    else
                    {
                        habitats.Add(new HabitatPlant
                        {
                            Plant = plant,
                            HabitatId = habitat
                        });
                    }
                }
            }
            if (request.Image != null)
            {
                plant.Image = _uploader.Upload(request.Image);
            }
            if (request.Price != null)
            {
                plant.Price = (decimal)request.Price;
            }
            plant.ModifiedAt = DateTime.UtcNow;
            Context.SaveChanges();
        }
    }
}
