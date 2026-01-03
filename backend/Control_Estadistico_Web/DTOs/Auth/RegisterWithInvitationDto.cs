using System.ComponentModel.DataAnnotations;

namespace Control_Estadistico_Web.DTOs.Auth
{
    public class RegisterWithInvitationDto
    {
        [Required]
        public string Token { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
