using Control_Estadistico_Web.DTOs;

namespace Control_Estadistico_Web.Services.Interfaces
{
    public interface IInstallationService
    {
        Task<InstallationDto> AddInstallationAsync(InstallationDto installationDto, Guid companyId);
        Task<InstallationDto> GetInstallationAsync(Guid id, Guid companyId);
        Task<List<InstallationDto>> GetAllInstallationAsync(Guid companyId);
        Task<InstallationDto> UpdateInstallationAsync(InstallationDto installationDto, Guid companyId);
        Task<bool> DeleteInstallationAsync(Guid id, Guid companyId);
    }
}
