using Control_Estadistico_Web.Data;
using Control_Estadistico_Web.Models;
using Control_Estadistico_Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Control_Estadistico_Web.Repositories.Implementations
{
    public class InstallationRepository : IInstallationRepository
    {
        private readonly ApplicationDbContext _context;

        public InstallationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //peticiones

        #region Create Installation
        public async Task<Installation> AddAsync(Installation installation)
        {
            _context.installations.Add(installation);
            await _context.SaveChangesAsync();
            return installation;
        }

        #endregion

        #region Get Installation By Id
        public async Task<Installation> GetByIdAsync(Guid id)
        {
            return await _context.installations.FindAsync(id);
        }

        #endregion

        #region Get All Installations
        public async Task<List<Installation>> GetAllByCompanyIdAsync(Guid companyId)
        {
            return await _context.installations
                .Where(i => i.CompanyId == companyId)
                .ToListAsync();
        }
        #endregion

        #region Update Installation
        public async Task<Installation> UpdateAsync(Installation installation)
        {
            _context.installations.Update(installation);
            await _context.SaveChangesAsync();
            return installation;
        }
        #endregion

        #region Delete Installation
        public async Task<bool> DeleteAsync(Guid id)
        {
            var installation = await _context.installations.FindAsync(id);

            if (installation == null)
                return false;

            _context.installations.Remove(installation);
            await _context.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
