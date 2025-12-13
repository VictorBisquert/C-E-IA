using AutoMapper;
using Control_Estadistico_Web.Data;
using Control_Estadistico_Web.DTOs;
using Control_Estadistico_Web.Mappings;
using Control_Estadistico_Web.Models;
using Control_Estadistico_Web.Repositories.Implementations;
using Control_Estadistico_Web.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_Estadistico_Web.Test.Services
{
    public class PesadasServiceTest
    {
        #region Variables
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PesadasServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ScaleProfile>();
            }, NullLoggerFactory.Instance);

            _mapper = config.CreateMapper();
        }
        #endregion

        #region Prueba simulación y error get scale

        [Fact]
        public async Task GetScaleAsync_ReturnsScale_WhenExists()
        {
            // 1) Configurar DbContext con InMemory
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // 2) Insertar un objeto de prueba 
                var testScale = new Scale
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    LastConnectionAt = DateTime.UtcNow,
                    Name = "BASC1",
                    IpAddress = "10.0.0.178",
                    Port = 23,
                    IsActive = true
                };

                context.scales.Add(testScale);
                await context.SaveChangesAsync();

                // 3) Instanciar repositorio y servicio
                var repo = new ScaleRepository(context);
                var service = new ScaleService(repo, _mapper);

                // 4) Ejecutar método
                var result = await service.GetScaleAsync(testScale.Id);

                // 5) Verificar resultado
                Assert.NotNull(result);
                Assert.Equal("BASC1", result.Name);
                Assert.Equal("10.0.0.178", result.IpAddress);
            }
        }

        #endregion

        #region Prueba simulación y error create scale
        [Fact]
        public async Task AddScaleAsync_Test()
        {
            // 1) Configurar DbContext con InMemory
            //simulamos una base de datos que se llama asi

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Insert")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // 2) Instanciar repo y servicio
                var _repo = new ScaleRepository(context);
                var _service = new ScaleService(_repo, _mapper);

                // 3) Crear un DTO de prueba
                var newScale = new ScaleDto
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    LastConnectionAt = DateTime.UtcNow,
                    Name = "BASC1",
                    IpAddress = "10.0.0.178",
                    Port = 23,
                    IsActive = true
                };

                // 4) Llamar al servicio
                var result = await _service.AddScaleAsync(newScale);

                // 5) Verificaciones
                Assert.NotNull(result);
                Assert.Equal("BASC1", result.Name);
                Assert.Equal("10.0.0.178", result.IpAddress);

                // 6) Verificar que realmente se insertó en la base de datos
                var savedScale = await context.scales.FindAsync(newScale.Id);
                Assert.NotNull(savedScale);
                Assert.Equal("BASC1", savedScale.Name);
            }
        }

        #endregion

        #region Prueba simulación y error getAll scale
        [Fact]
        public async Task GetAllScalesAsync_test()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_GetAll")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // 2) Insertar un objeto de prueba 
                var testScale1 = new Scale
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    LastConnectionAt = DateTime.UtcNow,
                    Name = "BASC1",
                    IpAddress = "10.0.0.178",
                    Port = 23,
                    IsActive = true
                };
                // 2) Insertar un objeto de prueba 
                var testScale2 = new Scale
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    LastConnectionAt = DateTime.UtcNow,
                    Name = "BASC2",
                    IpAddress = "10.0.0.179",
                    Port = 23,
                    IsActive = true
                };

                context.scales.Add(testScale1);
                context.scales.Add(testScale2);
                await context.SaveChangesAsync();

                var _repo = new ScaleRepository(context);
                var _service = new ScaleService(_repo, _mapper);

                var result = await _service.GetAllScaleAsync();

                // 5) Verificar resultado
                Assert.NotNull(result);

            }

        }

        #endregion

        #region Prueba simulación y error Update scale
        [Fact]
        public async Task UpdateScaleAsync_test()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Update")
                .Options;

            using (var context = new ApplicationDbContext(options)) 
            {
                // 2) Insertar un objeto original
                var originalScale = new Scale
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    LastConnectionAt = DateTime.UtcNow,
                    Name = "BASC1",
                    IpAddress = "10.0.0.178",
                    Port = 23,
                    IsActive = true
                };

                // 3) Crear el repo y el service
                context.scales.Add(originalScale);
                await context.SaveChangesAsync();

                var _repo = new ScaleRepository(context);
                var _service = new ScaleService(_repo, _mapper);

                // 4) Preparar un DTO modificado
                var updatedScaleDto = new ScaleDto
                {
                    Id = originalScale.Id,   // IMPORTANTE: mismo ID
                    Name = "BASC1_Modified",
                    IpAddress = "10.0.0.200",
                    Port = 25,
                    IsActive = false,
                    CreatedAt = originalScale.CreatedAt, // opcional
                    LastConnectionAt = DateTime.UtcNow
                };

                var result = await _service.UpdateScaleAsync(updatedScaleDto);

                // 6) Verificaciones
                Assert.NotNull(result);
                Assert.Equal("BASC1_Modified", result.Name);
                Assert.Equal("10.0.0.200", result.IpAddress);
                Assert.Equal(25, result.Port);

                // 7) Confirmar que se modificó en la base de datos
                var savedScale = await context.scales.FindAsync(originalScale.Id);
                Assert.NotNull(savedScale);
                Assert.Equal("BASC1_Modified", savedScale.Name);
                Assert.Equal("10.0.0.200", savedScale.IpAddress);
                Assert.False(savedScale.IsActive);
            }

        }
        #endregion

        #region Prueba simulación y error delete scale
        [Fact]
        public async Task DeleteScaleAsync_Test()
        {
            //creamos bd inmemory
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Delete")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // 2) Insertar un objeto de prueba 
                var testScale = new Scale
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    LastConnectionAt = DateTime.UtcNow,
                    Name = "BASC1",
                    IpAddress = "10.0.0.178",
                    Port = 23,
                    IsActive = true
                };

                context.scales.Add(testScale);
                await context.SaveChangesAsync();

                // 3) Instanciar repositorio y servicio
                var _repo = new ScaleRepository(context);
                var _service = new ScaleService(_repo, _mapper);

                // 4) Ejecutar método Delete
                var result = await _service.DeleteScaleAsync(testScale.Id);

                // 5) Verificar que devuelve true
                Assert.True(result);

                // 6) Confirmar que ya no existe en la base de datos
                var deletedEntity = await context.scales.FindAsync(testScale.Id);
                Assert.Null(deletedEntity);
            }
        }
        #endregion
    }
}
