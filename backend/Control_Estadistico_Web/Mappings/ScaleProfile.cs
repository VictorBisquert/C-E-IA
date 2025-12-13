using AutoMapper;
using Control_Estadistico_Web.DTOs;
using Control_Estadistico_Web.Models;

namespace Control_Estadistico_Web.Mappings
{
    public class ScaleProfile : Profile
    {
        public ScaleProfile() 
        {
            //mapeo basico
            CreateMap<Scale, ScaleDto>();

            //mapeo para crear nueva bascula
            CreateMap<ScaleDto, Scale>();
        }
    }
}
