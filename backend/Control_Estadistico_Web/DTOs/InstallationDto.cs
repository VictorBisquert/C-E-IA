namespace Control_Estadistico_Web.DTOs
{
    public class InstallationDto
    {
        public Guid Id { get; set; }  // Identificador de la instalación
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}
