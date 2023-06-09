using Application.UseCaseHandling;
using Application.UseCases.Queries.Searches;
using Application.UseCases.Queries;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private IQueryHandler _queryHandler;

        public LogController(IQueryHandler queryHandler, PlantPlanetContext context)
        {
            _queryHandler = queryHandler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] LogSearch search,
                                 [FromServices] ISearchLogQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, search));
        }
    }
}
