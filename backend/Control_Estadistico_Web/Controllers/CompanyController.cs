


using Control_Estadistico_Web.DTOs;
using Control_Estadistico_Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Control_Estadistico_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService companyService)
        {
            _service = companyService;
        }

        #region Get company por id
        // ------ Swagger ------\\
        /// <summary>
        /// Obtiene una empresa por su ID.
        /// </summary>
        /// /// <param name="id">El ID de la empresa.</param>
        /// <returns>Un objeto CompanyDTO si se encuentra.</returns>
        /// <response code="200">Devuelve la empresa solicitada</response>
        /// <response code="400">Si el id está vacío o es inválido</response>
        /// <response code="404">Si no se encuentra la empresa</response>
        [HttpGet("{id}")]         // Acción HTTP GET: recibe un 'id' en la URL (/api/Company/{id})
        public async Task<ActionResult<CompanyDto>> Get(Guid id)
        {
            // Valida que el id no esté vacío o nulo
            if (id == Guid.Empty)
                return BadRequest(new { message = "Id is required." });


            // Llama al service para obtener la empresa por id
            var dto = await _service.GetCompanyAsync(id);

            // Si no encuentra la empresa, devuelve 404
            if (dto == null)
                return NotFound(new { message = $"Company with id '{id}' not found." });

            // Si la encuentra, devuelve 200 OK con el DTO
            return Ok(dto); //200 + CompanyDto
        }
        #endregion

        #region Post company
        [HttpPost("createCompany")]
        public async Task<ActionResult<CompanyDto>> Post(CompanyDto companydto)
        {
            if (companydto == null)
                return BadRequest(new { message = "object is null" });// Devuelve 400 si id inválido

            var dto = await _service.AddCompanyAsync(companydto);

            return Ok(dto);
        }
        #endregion

        #region Get All Companies
        /// <summary>
        /// Obtiene todas las compañias.
        /// </summary>
        /// <returns>Una lista de CompanyDto.</returns>
        /// <response code="200">Devuelve las compañias solicitadas</response>
        /// <response code="404">Si no se encuentran compañias</response>

        [HttpGet("AllCompanys")]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAll()
        {
            var companies = await _service.GetAllCompaniesAsync();

            if (companies == null || !companies.Any())
            {
                return NotFound(new { message = "No se encontraron compañias." });
            }

            return Ok(companies);
        }

        #endregion

    }
}
