using Control_Estadistico_Web.DTOs.Auth;
using System.ComponentModel.DataAnnotations;

namespace Control_Estadistico_Web.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Logo { get; set; }
        [Required]
        public string Cif { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string Location { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public bool Active { get; set; } = true;

        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
