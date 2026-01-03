using System.ComponentModel.DataAnnotations;

namespace Control_Estadistico_Web.DTOs
{
    public class ScaleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastConnectionAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
