using Control_Estadistico_Web.Data;
using Control_Estadistico_Web.Models;
using Control_Estadistico_Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Control_Estadistico_Web.Repositories.Implementations
{
    public class CompanyRepository : ICompanyRepository
    {

        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Get company por id
        //Este es el método o función que busca una empresa por su ID.
        public async Task<Company> GetByIdAsync(Guid id)
        {
            //hacemos la busqueda en la tabla Company de la base de datos de manera asincrona.
            //Devolvemos el resultado como un objeto CompanyModel.
            return await _context.companies.FindAsync(id);
        }
        #endregion

        #region Get all companys
        //Este es el método o función que trae todas las empresas.
        public async Task<List<Company>> GetAllAsync()
        {
            return await _context.companies.ToListAsync();
        }
        #endregion

        #region Create company
        //Add
        public async Task<Company> AddAsync(Company company)
        {
            _context.companies.Add(company);       // 1) Le decimos a EF Core que "prepare" un INSERT de esta nueva compañía en la tabla Company.
            await _context.SaveChangesAsync();  // 2) Ejecuta realmente el INSERT en la base de datos (guarda los cambios).
            return company;                    // 3) Devuelve el objeto que acabamos de insertar (ya "existe" en la BD).
        }
        #endregion

        #region Update company
        //update
        public async Task<Company> UpdateAsync(Company company)
        {
            _context.companies.Update(company);
            await _context.SaveChangesAsync();
            return company;
        }
        #endregion

        #region Delete company
        //delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            //tenemos que traer primero el objeto o compañia por id
            //si nos retorna el objeto pues lo eliminamos

            var company = await _context.companies.FindAsync(id);
            if (company == null)
                return false; // si no existe, devolvemos false

            _context.companies.Remove(company);  // le dice a EF Core que borre esa entidad.
            await _context.SaveChangesAsync(); // ejecuta el DELETE en la base de datos
            return true;                       // confirma que se eliminó
        }
        #endregion
    }
}
