using Control_Estadistico_Web.Models;

namespace Control_Estadistico_Web.Repositories.Interfaces
{
    public interface IScaleRepository
    {
        //traer bascula por id
        Task<Scale> GetByIdAsync(Guid id);

        //crear una bascula
        Task<Scale> AddAsync(Scale scale);

        //traer todas las basculas
        Task<List<Scale>> GetAllScalesAsync();

        //modificar una bascula
        Task<Scale> UpdateScaleAsync(Scale scale);

        //borrar bascula
        Task<bool> DeleteScaleAsync(Guid id);
    }
}
