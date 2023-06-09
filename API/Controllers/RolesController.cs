using Application.UseCaseHandling;
using Application.UseCases.Queries.Searches;
using Application.UseCases.Queries;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private IQueryHandler _queryHandler;

        public RolesController(IQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Search search,
                                 [FromServices] IGetRoleQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, search));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindRoleQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, id));
        }
    }
}
