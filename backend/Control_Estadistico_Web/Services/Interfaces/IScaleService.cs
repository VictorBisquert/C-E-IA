using Control_Estadistico_Web.DTOs;

namespace Control_Estadistico_Web.Services.Interfaces
{
    public interface IScaleService
    {
        Task<ScaleDto> GetScaleAsync(Guid id);
        Task<ScaleDto> AddScaleAsync(ScaleDto scaleDto);
        Task<List<ScaleDto>> GetAllScaleAsync();
        Task<ScaleDto> UpdateScaleAsync(ScaleDto scale);
        Task<bool> DeleteScaleAsync(Guid id);
    }
}
