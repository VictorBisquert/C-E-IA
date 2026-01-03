using Control_Estadistico_Web.DTOs;

namespace Control_Estadistico_Web.Services.Interfaces
{
    public interface ICompanyService
    {
        //traer solo una compañia por id
        Task<CompanyDto> GetCompanyAsync(Guid id);

        //traer todas las compañias
        Task<List<CompanyDto>> GetAllCompaniesAsync();
        //Add
        Task<CompanyDto> AddCompanyAsync(CompanyDto companyDTO);
        //Update
        //Task<CompanyDto> UpdateCompanyAsync(CompanyDto companyDTO);
        //Delete
        //Task<bool> DeleteCompanyAsync(Guid id);
    }
}
