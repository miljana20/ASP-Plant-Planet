using Application.UseCases.Queries.Searches;
using Application.UseCases.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Application.UseCaseHandling;
using DataAccess;
using Application.UseCases.Commands;
using Application.UseCases.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitatController : ControllerBase
    {
        private IQueryHandler _queryHandler;
        private ICommandHandler _comandHandler;

        public HabitatController(IQueryHandler queryHandler, ICommandHandler comandHandler)
        {
            _queryHandler = queryHandler;
            _comandHandler = comandHandler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Search search,
                                 [FromServices] IGetHabitatQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, search));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindHabitatQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateCategoryDto dto, [FromServices] ICreateHabitatCommand command)
        {
            _comandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromServices] IUpdateHabitatCommand command, [FromBody] UpdateCategoryDto dto)
        {
            dto.Id = id;
            _comandHandler.HandleCommand(command, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteHabitatCommand command)
        {
            _comandHandler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
