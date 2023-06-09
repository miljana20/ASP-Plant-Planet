using Application.UseCaseHandling;
using Application.UseCases.Commands;
using Application.UseCases.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartPlantController : ControllerBase
    {

        private ICommandHandler _comandHandler;

        public CartPlantController(ICommandHandler comandHandler)
        {
            _comandHandler = comandHandler;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddToCartDto dto, [FromServices] IAddToCartCommand command)
        {
            _comandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IRemoveFromCartCommand command)
        {
            _comandHandler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
