using Control_Estadistico_Web.Data;
using Control_Estadistico_Web.DTOs.Auth;
using Control_Estadistico_Web.Models;
using Control_Estadistico_Web.Services.Auth;
using Control_Estadistico_Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Control_Estadistico_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        #region Registro
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterRequestDto request)
        {
            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest(new AuthResponseDto
                {
                    Succes = false,
                    Message = "Las contraseñas no coinciden"
                });
            }

            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            // 1️⃣ Crear compañía
            var company = new Company
            {
                Name = request.CompanyName,
                Logo = "", // o un path por defecto
                Cif = "DEFAULT_CIF",
                Address = "DEFAULT_ADDRESS",
                Location = "DEFAULT_LOCATION",
                Phone = "000000000",
                Email = "default@empresa.com"
            };

            _dbContext.companies.Add(company);
            await _dbContext.SaveChangesAsync();

            // 2️⃣ Crear usuario ADMIN asociado a la compañía
            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Username ?? request.Email,
                CompanyId = company.Id
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                await transaction.RollbackAsync();
                return BadRequest(new AuthResponseDto
                {
                    Succes = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                });
            }

            await _userManager.AddToRoleAsync(user, "Admin");

            await transaction.CommitAsync();

            var token = await GenerateJwtTokenAsync(user);

            return Ok(new AuthResponseDto
            {
                Succes = true,
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(24),
                Message = "Empresa y usuario creados correctamente"
            });
        }

        #endregion

        #region Login
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Unauthorized(new AuthResponseDto
                {
                    Succes = false,
                    Message = "Credenciales inválidas"
                });
            }

            var token = await GenerateJwtTokenAsync(user);

            return Ok(new AuthResponseDto
            {
                Succes = true,
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(24),
                Message = "Login con éxito"
            });
        }
        #endregion

        #region Usuario logeado
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult> Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "No se pudo obtener el ID del usuario" });
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                user.Id,
                user.Email,
                user.UserName,
                Roles = roles
            });
        }
        #endregion

        #region Asignar rol a usuario
        [Authorize(Roles = "Admin")]
        [HttpPost("assign-role")]
        public async Task<ActionResult> AssignRole([FromBody] AssignRoleDto request, [FromServices] AuthService authService)
        {
            var result = await authService.AssignRoleAsync(request.Email, request.Role);

            if (!result)
            {
                return BadRequest(new { message = "No se pudo asignar el rol" });
            }

            return Ok(new { message = $"Rol {request.Role} asignado correctamente a {request.Email}" });
        }
        #endregion

        #region Registro por invitación
        [HttpPost("register/invitation")]
        public async Task<ActionResult<AuthResponseDto>> RegisterWithInvitation(RegisterWithInvitationDto request)
        {
            var invitation = await _dbContext.Invitations
                .FirstOrDefaultAsync(i =>
                    i.Token == request.Token &&
                    !i.Used &&
                    i.ExpiresAt > DateTime.UtcNow);

            if (invitation == null)
            {
                return BadRequest(new AuthResponseDto
                {
                    Succes = false,
                    Message = "Invitación inválida o expirada"
                });
            }

            // Validación extra: email debe coincidir
            if (!string.Equals(invitation.Email, request.Email, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new AuthResponseDto
                {
                    Succes = false,
                    Message = "El email no coincide con la invitación"
                });
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                CompanyId = invitation.CompanyId
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new AuthResponseDto
                {
                    Succes = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                });
            }

            await _userManager.AddToRoleAsync(user, invitation.Role);

            invitation.Used = true;
            await _dbContext.SaveChangesAsync();

            var token = await GenerateJwtTokenAsync(user);

            return Ok(new AuthResponseDto
            {
                Succes = true,
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(24),
                Message = "Usuario registrado mediante invitación"
            });
        }
        #endregion

        #region Generar Token JWT
        private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("company_id", user.CompanyId.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion

        #region Invitación

        [Authorize(Roles = "Admin")]
        [HttpPost("invite")]
        public async Task<ActionResult> CreateInvitation([FromBody] CreateInvitationDto request,[FromServices] IEmailService emailService)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var adminUser = await _userManager.FindByIdAsync(userId);
            if (adminUser == null) return Unauthorized();

            var company = await _dbContext.companies
                .FirstAsync(c => c.Id == adminUser.CompanyId);

            var invitation = new Invitation
            {
                CompanyId = adminUser.CompanyId,
                Email = request.Email,
                Role = request.Role,
                Token = Guid.NewGuid().ToString(),
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            _dbContext.Invitations.Add(invitation);
            await _dbContext.SaveChangesAsync();

            // 📧 Enviar email
            await emailService.SendInvitationEmailAsync(
                invitation.Email,
                company.Name,
                invitation.Token
            );

            return Ok(new
            {
                message = "Invitación enviada correctamente"
            });
        }
        #endregion
    }
}