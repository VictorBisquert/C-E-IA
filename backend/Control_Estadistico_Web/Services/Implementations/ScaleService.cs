using AutoMapper;
using Control_Estadistico_Web.DTOs;
using Control_Estadistico_Web.Models;
using Control_Estadistico_Web.Repositories.Implementations;
using Control_Estadistico_Web.Repositories.Interfaces;
using Control_Estadistico_Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Control_Estadistico_Web.Services.Implementations
{
    public class ScaleService : IScaleService
    {
        private readonly IScaleRepository _scaleRepository;
        private readonly IMapper _mapper;

        public ScaleService(IScaleRepository scaleRepository, IMapper mapper)
        {
            _scaleRepository = scaleRepository;
            _mapper = mapper;
        }

        //traer bascula por id
        public async Task<ScaleDto> GetScaleAsync(Guid id)
        {
            var scale = await _scaleRepository.GetByIdAsync(id);

            if (scale == null)
            {
                return null;
            }

            return _mapper.Map<ScaleDto>(scale);
        }

        //crear bascula
        public async Task<ScaleDto> AddScaleAsync(ScaleDto scale)
        {
            //aqui mapeamos porque vamos a insertar o post en tabla, entonces hay que mapear el dto con el model
            //Mapeamos el DTO al modelo que entiende EF Core
            var scaleModel = _mapper.Map<Scale>(scale);

            var savedScale = await _scaleRepository.AddAsync(scaleModel);

            return _mapper.Map<ScaleDto>(savedScale);

        }

        //traer todas las basculas
        public async Task<List<ScaleDto>> GetAllScaleAsync()
        {
            var list = await _scaleRepository.GetAllScalesAsync();

            if (list.Count <= 0) return null;

            return _mapper.Map<List<ScaleDto>>(list);
        }

        //modificar una bascula
        public async Task<ScaleDto> UpdateScaleAsync(ScaleDto scaleDto) 
        {
            //aqui mapeamos porque vamos a insertar o post en tabla, entonces hay que mapear el dto con el model
            var scaleModel = _mapper.Map<Scale>(scaleDto);

            var savedScale = await _scaleRepository.UpdateScaleAsync(scaleModel);

            return _mapper.Map<ScaleDto>(savedScale);

        }

        //borrar una bascula
        public async Task<bool> DeleteScaleAsync(Guid id)
        {
            // 1) Buscar la bascula en la BD
            var scale = await _scaleRepository.GetByIdAsync(id);
            // 2) Si no existe, devolvemos false
            if (scale == null) return false;
            // 3) Si existe, la eliminamos usando el repositorio
            await _scaleRepository.DeleteScaleAsync(id);
            // 4) Devolvemos true para indicar que se eliminó correctamente
            return true;
        }
    }
}
