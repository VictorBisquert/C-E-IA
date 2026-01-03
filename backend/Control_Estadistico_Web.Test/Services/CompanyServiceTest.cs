using AutoMapper;
using Control_Estadistico_Web.Data;
using Control_Estadistico_Web.DTOs;
using Control_Estadistico_Web.Mappings;
using Control_Estadistico_Web.Models;
using Control_Estadistico_Web.Repositories.Implementations;
using Control_Estadistico_Web.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_Estadistico_Web.Test.Services
{
    public class CompanyServiceTest
    {
        #region Variables
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public CompanyServiceTest()
        {
            // Configurar AutoMapper con tu Profile
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CompanyProfile>();
            }, NullLoggerFactory.Instance);

            _mapper = config.CreateMapper();
        }
        #endregion

        #region Prueba simulación y error get company
        [Fact]
        public async Task GetCompanyAsync_ReturnsCompany_WhenExists()
        {
            // 1) Configurar DbContext con InMemory
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // 2) Insertar un objeto de prueba 
                var testCompany = new Company
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Name = "OpenAI",
                    Active = true,
                    Logo = "logo.png",
                    Cif = "12345678A",
                    Address = "San Francisco",
                    Location = "California",
                    Phone = "1234567890",
                    Email = "info@openai.com"
                };

                context.companies.Add(testCompany);
                await context.SaveChangesAsync();

                // 3) Instanciar repositorio y servicio
                var repo = new CompanyRepository(context);
                var service = new CompanyService(repo, _mapper);

                // 4) Ejecutar método
                var result = await service.GetCompanyAsync(testCompany.Id);

                // 5) Verificar resultado
                Assert.NotNull(result);
                Assert.Equal("OpenAI", result.Name);
                Assert.Equal("info@openai.com", result.Email);
            }
        }
        #endregion

        #region Prueba simulación y error create company
        [Fact]
        public async Task AddCompanyAsync_Test()
        {
            // 1) Configurar DbContext con InMemory
            //simulamos una base de datos que se llama asi

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Insert")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // 2) Instanciar repo y servicio
                var _repo = new CompanyRepository(context);
                var _service = new CompanyService(_repo, _mapper);

                // 3) Crear un DTO de prueba
                var testCompany = new CompanyDto
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Name = "OpenAI",
                    Active = true,
                    Logo = "logo.png",
                    Cif = "12345678A",
                    Address = "San Francisco",
                    Location = "California",
                    Phone = "1234567890",
                    Email = "info@openai.com"
                };

                // 4) Llamar al servicio
                var result = await _service.AddCompanyAsync(testCompany);

                // 5) Verificaciones
                Assert.NotNull(result);
                Assert.Equal("OpenAI", result.Name);
                Assert.Equal("12345678A", result.Cif);

                // 6) Verificar que realmente se insertó en la base de datos
                var savedCompany = await context.companies.FindAsync(testCompany.Id);
                Assert.NotNull(savedCompany);
                Assert.Equal("OpenAI", savedCompany.Name);
            }
        }

        #endregion

        #region Prueba simulación y error getAll company
        [Fact]
        public async Task GetAllompaniesAsync_test()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_GetAll")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // 2) Insertar un objeto de prueba 
                var testCompany1 = new Company
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Name = "OpenAI",
                    Active = true,
                    Logo = "logo.png",
                    Cif = "12345678A",
                    Address = "San Francisco",
                    Location = "California",
                    Phone = "1234567890",
                    Email = "info@openai.com"
                };
                // 2) Insertar un objeto de prueba 
                var testCompany2 = new Company
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Name = "Google",
                    Active = true,
                    Logo = "logo_Google.png",
                    Cif = "87654321B",
                    Address = "San Francisco",
                    Location = "California",
                    Phone = "0987654321",
                    Email = "info@google.com"
                };

                context.companies.Add(testCompany1);
                context.companies.Add(testCompany2);
                await context.SaveChangesAsync();

                var _repo = new CompanyRepository(context);
                var _service = new CompanyService(_repo, _mapper);

                var result = await _service.GetAllCompaniesAsync();

                // 5) Verificar resultado
                Assert.NotNull(result);

            }

        }

        #endregion

    }
}
