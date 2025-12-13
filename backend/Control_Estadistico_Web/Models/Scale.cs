using System.ComponentModel.DataAnnotations;

namespace Control_Estadistico_Web.Models
{
    public class Scale
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string IpAddress { get; set; } = string.Empty;
        [Required]
        public int Port { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastConnectionAt { get; set; }
    }
}
