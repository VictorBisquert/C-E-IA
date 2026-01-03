using Control_Estadistico_Web.Models;

namespace Control_Estadistico_Web.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company> GetByIdAsync(Guid id);
        //Se podría añadir Add/Update/Delete/List según necesites
        Task<List<Company>> GetAllAsync();
        //método para crear/añadir una compañía
        Task<Company> AddAsync(Company company);
        //método para editar una compañia
        Task<Company> UpdateAsync(Company company);
        //método para borrar una compañia
        Task<bool> DeleteAsync(Guid id); // true si se eliminó, false si no existía
    }
}
