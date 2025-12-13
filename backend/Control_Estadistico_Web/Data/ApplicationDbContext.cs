using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Control_Estadistico_Web.Models;

namespace Control_Estadistico_Web.Data 
{ 
    public class ApplicationDbContext : IdentityDbContext 
    { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }

        //tablas en bd
        public DbSet<Scale> scales { get; set; }
    } 
}