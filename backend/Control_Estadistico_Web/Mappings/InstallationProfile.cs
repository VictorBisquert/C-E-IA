using AutoMapper;
using Control_Estadistico_Web.DTOs;
using Control_Estadistico_Web.Models;

namespace Control_Estadistico_Web.Mappings
{
    public class InstallationProfile : Profile
    {
        public InstallationProfile() 
        {
            CreateMap<Installation, InstallationDto>();

            //Ejemplo de como podria crecer CompanyProfile:

            // Mapeo para crear nueva empresa
            CreateMap<InstallationDto, Installation>();
            //CreateMap<CompanyCreateDTO, CompanyModel>();

            // Mapeo para actualizar parcialmente
            //CreateMap<CompanyUpdateDTO, CompanyModel>();
        }
    }
}
