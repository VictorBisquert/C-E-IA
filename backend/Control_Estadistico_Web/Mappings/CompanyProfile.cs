using AutoMapper;
using Control_Estadistico_Web.Models;
using Control_Estadistico_Web.DTOs;

namespace Control_Estadistico_Web.Mappings
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile() 
        {
            CreateMap<Company, CompanyDto>();

            //Ejemplo de como podria crecer CompanyProfile:

            // Mapeo para crear nueva empresa
            CreateMap<CompanyDto, Company>();
            //CreateMap<CompanyCreateDTO, CompanyModel>();

            // Mapeo para actualizar parcialmente
            //CreateMap<CompanyUpdateDTO, CompanyModel>();
        }
    }
}
