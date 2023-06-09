using Application.UseCaseHandling;
using Application.UseCases.Queries.Searches;
using Application.UseCases.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DataAccess;
using Application.UseCases.Commands;
using Application.UseCases.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private IQueryHandler _queryHandler;
        private ICommandHandler _comandHandler;

        public PlantController(IQueryHandler queryHandler, ICommandHandler comandHandler)
        {
            _queryHandler = queryHandler;
            _comandHandler = comandHandler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PlantSearch search,
                                 [FromServices] IGetPlantQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, search));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindPlantQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreatePlantDto dto, [FromServices] ICreatePlantCommand command)
        {
            _comandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromServices] IUpdatePlantCommand command, [FromBody] UpdatePlantDto dto)
        {
            dto.Id = id;
            _comandHandler.HandleCommand(command, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeletePlantCommand command)
        {
            _comandHandler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
