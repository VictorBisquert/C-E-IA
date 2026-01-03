using Control_Estadistico_Web.DTOs;
using Control_Estadistico_Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

        #region Get all scales
        /// <summary>
        /// Obtiene todas las básculas.
        /// </summary>
        /// <returns>Una lista de ScaleDto.</returns>
        /// <response code="200">Devuelve las básculas solicitadas</response>
        /// <response code="404">Si no se encuentran básculas</response>

        [Authorize]
        [HttpGet("AllScales")]
        public async Task<ActionResult<IEnumerable<ScaleDto>>> GetAll()
        {
            var scales = await _service.GetAllScaleAsync();

            if (scales == null || !scales.Any())
            {
                return NotFound(new { message = "No se encontraron básculas." });
            }

            return Ok(scales);
        }

        #endregion

        #region traer bascula por id
        [Authorize]
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
        #endregion

        #region Crear bascula
        [Authorize]
        [HttpPost("createScale")]
        public async Task<ActionResult<ScaleDto>> CreateScale(ScaleDto scale)
        {
            if (scale == null)
            {
                return BadRequest(new { message = "Null Object" });
            }

            var dto = await _service.AddScaleAsync(scale);

            return Ok(dto);
        }
        #endregion

        #region Update scale
        [Authorize]
        [HttpPut("updateScale")]
        public async Task<ActionResult<ScaleDto>> UpdateScale(ScaleDto scale)
        {
            if (scale == null)
            {
                return BadRequest(new { message = "Null Object" });
            }

            var dto = await _service.UpdateScaleAsync(scale);

            return Ok(dto);
        }
        #endregion

        #region Delete scale
        [Authorize]
        [HttpDelete("deleteScale/{id}")]
        public async Task<ActionResult<bool>> DeleteScale(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "Id is required." });

            var dto = await _service.GetScaleAsync(id);

            if (dto == null)
                return NotFound(new { message = $"Scale with id '{id}' not found." });

            var dtoDel = await _service.DeleteScaleAsync(id);

            return Ok();

        }

        #endregion

    }
}
