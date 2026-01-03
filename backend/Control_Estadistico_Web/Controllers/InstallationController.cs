using Control_Estadistico_Web.DTOs;
using Control_Estadistico_Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Control_Estadistico_Web.Extensions;

namespace Control_Estadistico_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstallationController : ControllerBase
    {
        #region variables
        private readonly IInstallationService _installationService;
        #endregion

        #region constructor
        public InstallationController(IInstallationService installationService)
        {
            _installationService = installationService;
        }

        #endregion

        #region create installation
        [Authorize(Roles = "Admin")]
        [HttpPost("createInstallation")]
        public async Task<ActionResult<InstallationDto>> Post([FromBody] InstallationDto installation)
        {
            if (installation == null)
                return BadRequest(new { message = "object is null" });

            var companyId = User.GetCompanyId();

            var dto = await _installationService.AddInstallationAsync(
                installation,
                companyId
            );

            return Ok(dto);
        }

        #endregion

        #region Get installation by id
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<InstallationDto>> GetInstallation(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "Id is required." });
            }

            // Obtener CompanyId desde los claims del usuario
            Guid companyId;
            try
            {
                companyId = User.GetCompanyId(); // tu extensión ClaimsPrincipalExtensions
            }
            catch
            {
                return BadRequest(new { message = "CompanyId claim not found." });
            }

            // Llamar al servicio pasando el companyId
            var installation = await _installationService.GetInstallationAsync(id, companyId);

            if (installation == null)
            {
                return NotFound(new { message = "Installation not found or does not belong to your company." });
            }

            return Ok(installation);
        }

        #endregion

        #region Get All Installations
        [Authorize(Roles = "Admin")]
        [HttpGet("AllInstallations")]
        public async Task<ActionResult<List<InstallationDto>>> GetAll()
        {
            Guid companyId;

            try
            {
                companyId = User.GetCompanyId();
            }
            catch
            {
                return Unauthorized(new { message = "CompanyId claim not found." });
            }

            var installations = await _installationService
                .GetAllInstallationAsync(companyId);

            return Ok(installations);
        }
        #endregion

        #region Update installation
        [Authorize(Roles = "Admin")]
        [HttpPut("updateInstallation")]
        public async Task<ActionResult<InstallationDto>> UpdateInstallation(InstallationDto installationDto)
        {
            if (installationDto == null)
            {
                return BadRequest(new { message = "Body is required." });
            }

            Guid companyId;
            try
            {
                companyId = User.GetCompanyId();
            }
            catch
            {
                return Unauthorized(new { message = "CompanyId claim not found." });
            }

            try
            {
                var updated = await _installationService.UpdateInstallationAsync(
                    installationDto,
                    companyId
                );

                return Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                // Proviene del guard / service
                return Forbid(ex.Message);
            }
        }
        #endregion

        #region Delete installation
        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteInstallation/{id}")]
        public async Task<ActionResult<bool>> DeleteInstallationAsync(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "Id is required" });

            Guid companyId;

            try
            {
                companyId = User.GetCompanyId();
            }
            catch
            {
                return BadRequest(new { message = "CompanyId claim not found." });
            }

            var dto = await _installationService.GetInstallationAsync(id, companyId);

            if(dto == null)
                return NotFound(new { message = $"Installation with id '{id}' not found." });

            var dtoDel = await _installationService.DeleteInstallationAsync(id, companyId);

            return Ok();

        }

        #endregion
    }
}
