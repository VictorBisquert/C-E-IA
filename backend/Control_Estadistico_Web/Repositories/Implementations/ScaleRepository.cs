
using Control_Estadistico_Web.Data;
using Control_Estadistico_Web.Models;
using Control_Estadistico_Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Control_Estadistico_Web.Repositories.Implementations
{
    public class ScaleRepository : IScaleRepository
    {

        private readonly ApplicationDbContext _context;

        public ScaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //get bascula por id
        public async Task<Scale> GetByIdAsync(Guid id)
        {
            return await _context.scales.FindAsync(id);
        }

        //crear una bascula
        public async Task<Scale> AddAsync(Scale scale)
        {
            _context.scales.Add(scale);// 1) Le decimos a EF Core que "prepare" un INSERT de esta nueva bascula en la tabla Scale.
            await _context.SaveChangesAsync();// 2) Ejecuta realmente el INSERT en la base de datos (guarda los cambios).
            return scale;// 3) Devuelve el objeto que acabamos de insertar (ya "existe" en la BD).
        }

        //traer todas las basculas
        public async Task<List<Scale>> GetAllScalesAsync()
        {
            return await _context.scales.ToListAsync();
        }

        //modificar una bascula
        public async Task<Scale> UpdateScaleAsync(Scale scale)
        {
            // Buscar la entidad existente
            var existingScale = await _context.scales.FindAsync(scale.Id);
            if (existingScale == null)
                throw new InvalidOperationException("Scale not found");

            // Actualizar las propiedades necesarias
            existingScale.Name = scale.Name;
            existingScale.IpAddress = scale.IpAddress;
            existingScale.Port = scale.Port;
            existingScale.IsActive = scale.IsActive;
            existingScale.LastConnectionAt = scale.LastConnectionAt;
            existingScale.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingScale;
        }

        //borrar bascula
        public async Task<bool> DeleteScaleAsync(Guid id)
        {
            var scale = await _context.scales.FindAsync(id);
            if (scale == null)
                return false;

            _context.scales.Remove(scale);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
