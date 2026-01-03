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
    public class InstallationServiceTest
    {
        #region variables
        private readonly IMapper _mapper;
        #endregion

        #region constructor
        public InstallationServiceTest()
        {
            // Configurar AutoMapper con tu Profile
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<InstallationProfile>();
            }, NullLoggerFactory.Instance);

            _mapper = config.CreateMapper();
        }

        #endregion

        #region Prueba simulación y error create installation
        [Fact]
        public async Task AddInstallationAsync_Should_CreateInstallation_WithCompanyId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);

            // Crear company
            var companyId = Guid.NewGuid();
            context.companies.Add(new Company
            {
                Id = companyId,
                Name = "Empresa Test",
                CreatedAt = DateTime.UtcNow,
                Logo = "logo-default.png",
            });
            await context.SaveChangesAsync();

            var repo = new InstallationRepository(context);
            var service = new InstallationService(repo, _mapper);

            var dto = new InstallationDto
            {
                Name = "Instalacion 1",
                Address = "calle prueba",
                Location = "Valencia",
                City = "Valencia",
                Active = true
            };

            var result = await service.AddInstallationAsync(dto, companyId);

            Assert.NotNull(result);
            Assert.Equal("Instalacion 1", result.Name);

            var saved = await context.installations.FirstOrDefaultAsync();
            Assert.NotNull(saved);
            Assert.Equal(companyId, saved.CompanyId);
        }

        #endregion

        #region Prueba simulación y error get installation by id
        [Fact]
        public async Task GetScaleAsync_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);

            // Crear company
            var companyId = Guid.NewGuid();
            context.companies.Add(new Company
            {
                Id = companyId,
                Name = "Empresa Test",
                CreatedAt = DateTime.UtcNow,
                Logo = "logo-default.png",
            });

            // Crear instalación
            var installationId = Guid.NewGuid();
            context.installations.Add(new Installation
            {
                Id = installationId,
                Name = "Instalacion Test",
                Address = "Calle X",
                Location = "Valencia",
                City = "Valencia",
                Active = true,
                CompanyId = companyId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            await context.SaveChangesAsync();

            var repo = new InstallationRepository(context);
            var service = new InstallationService(repo, _mapper);

            // Act
            var result = await service.GetInstallationAsync(installationId, companyId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Instalacion Test", result!.Name);
            //Assert.Equal(companyId, result.CompanyId);
        }


        #endregion

    }
}
