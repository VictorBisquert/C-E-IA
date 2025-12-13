using Control_Estadistico_Web.DTOs;
using Control_Estadistico_Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Control_Estadistico_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScalesController : ControllerBase
    {
        private readonly IScaleService _service;

        public ScalesController(IScaleService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScaleDto>> GetScale(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "Id is required."});
            }

            var scale = await _service.GetScaleAsync(id);

            if (scale == null)
            {
                return NotFound(new { message = $"Scale with id '{id}' not found."});
            }

            return Ok(scale); //200 + scaledto
        }
    }
}
