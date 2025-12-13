using Azure;
using Control_Estadistico_Web.DTOs.Auth;
using Control_Estadistico_Web.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Control_Estadistico_Web.Controllers
{
    /*
    ControllerBase da funcionalidades para construir API REST, como:

    Ok() → devuelve respuesta correcta.

    BadRequest() → devuelve error 400.

    Unauthorized() → devuelve error 401.

    ActionResult<T> → devuelve objetos y estados HTTP.
    */

    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        #region Variables
        /*
         UserManager es una clase de Identity que se encarga de gestionar usuarios.
         Sirve para acciones como:
          * Crear usuarios
          * Buscar usuarios por email, nombre, id
          * Validar contraseñas
          * Editar o eliminar usuarios
        */
        private readonly UserManager<IdentityUser> _userManager;

        /*
         Sirve para leer valores del appsettings.json o variables de entorno.
         Se usa principalmente para leer la clave del JWT, el issuer, audience, etc.
        */
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        #endregion

        #region Registro
        //Función para registrarse un usuario
        /*
        El método es asíncrono (async).

        Recibe un objeto RegisterRequestDto, que contiene los datos del formulario (email, password, etc.).

        Devuelve una respuesta AuthResponseDto con estado HTTP (Ok, BadRequest, etc.).
        */
        // api/auth/register
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterRequestDto request)
        {
            //validamos que la contraseña coincida
            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest(new AuthResponseDto
                {
                    Succes = false,
                    Message = "Las contraseñas no coinciden"
                });
            }
            //Crea un usuario de Identity.
            //Si no mandan un Username, usa el email como nombre de usuario
            var user = new IdentityUser
            {
                Email = request.Email,
                UserName = request.Username ?? request.Email,
            };

            //Usa Identity para guardar el usuario junto con su contraseña encriptada
            var result = await _userManager.CreateAsync(user, request.Password);
            //Asignamos el rol de User por defecto al registrarse
            await _userManager.AddToRoleAsync(user, "User");

            //Si algo falló (contraseña débil, email duplicado, etc.),
            //devuelve BadRequest con los errores.
            if (!result.Succeeded)
            {
                return BadRequest(new AuthResponseDto
                {
                    Succes = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                });
            }

            //Si todo estuvo bien, responde con 200 OK
            //y un mensaje indicando que el usuario se registró
            return Ok(new AuthResponseDto
            {
                Succes = true,
                Message = "Usuario registrado correctamente"
            });
        }
        #endregion

        #region Login
        //devuelve un ActionResult<AuthResponseDto> (un DTO con el resultado del login y el token si todo va bien)
        //Función para logearse un usuario
        // api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginRequestDto request)
        {
            //Usa UserManager<IdentityUser> (parte de ASP.NET Identity) para buscar en la base de datos un usuario con ese email
            var user = await _userManager.FindByEmailAsync(request.Email);

            //Si no existe el usuario o la contraseña no coincide (CheckPasswordAsync compara la contraseña con la almacenada en la BD de forma segura), se responde con 401 Unauthorized y un AuthResponseDto indicando error
            //CheckPasswordAsync ya compara hashes y sal(no comparas el texto plano tú)
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Unauthorized(new AuthResponseDto
                {
                    Succes = false,
                    Message = "Credenciales inválidad"
                });
            }

            //Llama a una función que crea el JWT (cadena compacta que el cliente usará para autenticarse en siguientes peticiones)
            var token = GenerateJwtToken(user);

            //Devuelve 200 OK con:
            //Token: la cadena JWT.
            //Expiration: la fecha/ hora en UTC cuando caduca(aquí 24 horas).
            //Succes y Message: info legible para el cliente.
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
            var user = await _userManager.FindByIdAsync(userId);

            return Ok(new { user.Id, user.Email, user.UserName });
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

        #region Función que genera token del usuario
        //Funcion que genera el token
        private string GenerateJwtToken(IdentityUser user)
        {
            //Claims = pares clave/valor que irán en el payload del token. Ejemplos:
            //sub(subject): email del usuario.
            //jti: id único del token(evita duplicados / ayuda a revocación).
            //NameIdentifier y Name: id y nombre de usuario.
            //Estas claims permiten al servidor identificar al usuario cuando valide el token.
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName!)
            };

            //Lee Jwt:Key desde appsettings (o secrets)
            //Convierte la cadena en bytes y crea una clave simétrica(SymmetricSecurityKey) que se usará para firmar el token(HMAC)
            //Importante: esta clave debe ser larga y segura(mínimo 256 bits recomendado para HS256) y no guardarla en el código
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key no configurada")));

            //Indica cómo se firmará el token: aquí HMAC-SHA256 usando la key
            //Firmar el token garantiza que quien lo recibe pueda verificar que el token lo generó tu servidor(y que no ha sido modificado)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            /*
            Construye el objeto JwtSecurityToken con:
             * issuer: quién emite el token (normalmente tu app / dominio).
             * audience: para quién es (tu API u otros consumidores).
             * claims: los datos del usuario.
             * expires: cuándo caduca.
             * signingCredentials: la forma de firmarlo.
            Nota importante: aquí en tu código se está usando _configuration["Jwt:Key"] como issuer. Eso parece un error: issuer debería ser algo como "MiApp" o una URL, no la clave secreta. (Lo correcto es issuer: _configuration["Jwt:Issuer"]).
            */
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds
            );

            //Convierte el token a la cadena compacta que se envía al cliente (formato xxxxx.yyyyy.zzzzz).
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        #endregion

    }
}
