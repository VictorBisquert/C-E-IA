using Control_Estadistico_Web.Models;

namespace Control_Estadistico_Web.Repositories.Interfaces
{
    public interface IInstallationRepository
    {
        Task<Installation> GetByIdAsync(Guid id);
        //Se podría añadir Add/Update/Delete/List según necesites
        Task<List<Installation>> GetAllByCompanyIdAsync(Guid companyId);
        //método para crear/añadir una compañía
        Task<Installation> AddAsync(Installation installation);
        //método para editar una compañia
        Task<Installation> UpdateAsync(Installation installation);
        //método para borrar una compañia
        Task<bool> DeleteAsync(Guid id); // true si se eliminó, false si no existía
    }
}
