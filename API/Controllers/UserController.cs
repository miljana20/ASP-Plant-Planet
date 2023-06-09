using Application.UseCaseHandling;
using Application.UseCases.Queries.Searches;
using Application.UseCases.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using DataAccess;
using System.Linq;
using Application.UseCases.Commands;
using Application.UseCases.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IQueryHandler _queryHandler;
        private ICommandHandler _comandHandler;

        public UserController(IQueryHandler queryHandler, ICommandHandler comandHandler)
        {
            _queryHandler = queryHandler;
            _comandHandler = comandHandler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Search search,
                                 [FromServices] IGetUserQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, search));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindUserQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] RegisterUserDto dto, [FromServices] IRegisterUserCommand command)
        {
            _comandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteUserCommand command)
        {
            _comandHandler.HandleCommand(command, id);
            return NoContent();
        }

    }
}
