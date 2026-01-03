using Control_Estadistico_Web.DTOs.Auth;

namespace Control_Estadistico_Web.DTOs
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Cif { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
