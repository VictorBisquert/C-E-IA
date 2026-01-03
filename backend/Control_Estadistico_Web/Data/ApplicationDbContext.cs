using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Control_Estadistico_Web.Models;
using Control_Estadistico_Web.DTOs.Auth;

namespace Control_Estadistico_Web.Data 
{ 
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }

        //tablas en bd
        public DbSet<Scale> scales { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<Invitation> Invitations => Set<Invitation>();
        public DbSet<Installation> installations {  get; set; }
    } 
}