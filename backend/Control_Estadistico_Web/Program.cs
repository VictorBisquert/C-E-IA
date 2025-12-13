using Control_Estadistico_Web.Data;
using Control_Estadistico_Web.Mappings;
using Control_Estadistico_Web.Middleware;
using Control_Estadistico_Web.Repositories.Implementations;
using Control_Estadistico_Web.Repositories.Interfaces;
using Control_Estadistico_Web.Services.Implementations;
using Control_Estadistico_Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// **************************
// * Estructura del Program *
// **************************

// ****** Registrar servicios ****** \\

//Aquí va AddDbContext, AddIdentity, AddAuthentication, AddControllers, etc.

// ===== Configurar DbContext con SQL Server =====
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// ===== Configurar Identity =====
// Sistema de autenticación y manejo de usuarios de .NET
// Con el siguiente código activamos identity en el proyecto
// IdentityUser ? representa un usuario (ejemplo: nombre, email, contraseña).
// IdentityRole ? representa un rol (ejemplo: "Admin", "Usuario").
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>() //Esto le dice a Identity dónde guardar los datos de usuarios y roles.
    .AddDefaultTokenProviders(); // Agrega proveedores de tokens por defecto. Uso como restablecer contraseña, confirmar email,verificación de dos factores (2FA).

// ===== Configurar Autenticación JWT (va después de Identity) =====
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// ===== AutoMapper =====
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ScaleProfile>();
});

// ===== Repositorios y Servicios =====
builder.Services.AddScoped<IScaleRepository, ScaleRepository>();
builder.Services.AddScoped<IScaleService, ScaleService>();

// ===== Controllers y Swagger =====
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== CORS =====
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

var app = builder.Build();


//********************************************************************************************\\

// ****** Configurar el pipeline de la aplicación ****** \\

// Aquí van los UseAuthentication, UseAuthorization, UseSwagger, etc.

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthentication(); // Debe ir ANTES de UseAuthorization()
app.UseAuthorization();

app.MapControllers();

app.Run();
