using AutoMapper;
using Control_Estadistico_Web.DTOs;
using Control_Estadistico_Web.Repositories.Interfaces;
using Control_Estadistico_Web.Services.Interfaces;
using Control_Estadistico_Web.Models;

namespace Control_Estadistico_Web.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        #region Get company por id
        //Recibe un id
        //Le pide al repositorio (_companyRepository) que busque la empresa en la base de datos.
        //Si no encuentra nada devuelve null.
        //Si encuentra la empresa usa AutoMapper (_mapper) para convertir la entidad de la base de datos (CompanyModel) a un objeto de transferencia (CompanyDTO) y lo devuelve.
        public async Task<CompanyDto> GetCompanyAsync(Guid id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null) return null;
            return _mapper.Map<CompanyDto>(company);
        }
        #endregion

        #region Create Company
        //Add
        public async Task<CompanyDto> AddCompanyAsync(CompanyDto companydto)
        {
            //Mapeamos el DTO al modelo que entiende EF Core
            var companyModel = _mapper.Map<Company>(companydto);

            var savedCompany = await _companyRepository.AddAsync(companyModel);

            return _mapper.Map<CompanyDto>(savedCompany);
        }
        #endregion

        #region get all companies
        public async Task<List<CompanyDto>> GetAllCompaniesAsync()
        {
            var list = await _companyRepository.GetAllAsync();

            if (list.Count() <= 0)
            {
                return null;
            }

            return _mapper.Map<List<CompanyDto>>(list);
        }
        #endregion

    }
}
