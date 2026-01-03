using System.ComponentModel.DataAnnotations;

namespace Control_Estadistico_Web.DTOs.Auth
{
    public class CreateInvitationDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "User"; // Por defecto 'User'
    }
}
