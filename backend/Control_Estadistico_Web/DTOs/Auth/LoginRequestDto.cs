/*
Qué es y por qué lo usas

Es también una clase C#, pero diseñada para lo que expones/recibes en la API (JSON).

Puede contener menos, más o distinto contenido que la entidad: por ejemplo, ocultas campos sensibles o combinas/renombras propiedades.

Ventajas: desacoplas la forma interna (Entities) de la interfaz pública, mejoras seguridad, controlas versiones y payloads más ligeros.

Idea simple: DTO = la forma pública y segura de los datos que envías/recibes por la API.
 */
using System.ComponentModel.DataAnnotations;

namespace Control_Estadistico_Web.DTOs.Auth
{
    public class LoginRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
