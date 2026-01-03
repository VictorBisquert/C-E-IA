using Control_Estadistico_Web.Models;
using Microsoft.AspNetCore.Identity;

namespace Control_Estadistico_Web.DTOs.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public Guid CompanyId { get; set; }

        public Company Company { get; set; } = null!;

        public Guid? InstallationId { get; set; }
    }
}
