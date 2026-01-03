using System.ComponentModel.DataAnnotations;

namespace Control_Estadistico_Web.Models
{
    public class Invitation
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "User";

        [Required]
        public string Token { get; set; } = string.Empty;

        public bool Used { get; set; } = false;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(7); // por defecto 7 días
    }
}
