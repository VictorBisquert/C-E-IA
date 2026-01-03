using System.ComponentModel.DataAnnotations;

namespace Control_Estadistico_Web.Models
{
    public class Installation
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
    }
}
