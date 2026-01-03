using AutoMapper;
using Control_Estadistico_Web.DTOs;
using Control_Estadistico_Web.Guards;
using Control_Estadistico_Web.Models;
using Control_Estadistico_Web.Repositories.Implementations;
using Control_Estadistico_Web.Repositories.Interfaces;
using Control_Estadistico_Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Control_Estadistico_Web.Services.Implementations
{
    public class InstallationService : IInstallationService
    {
        private readonly IInstallationRepository _installationRepository;
        private readonly IMapper _mapper;

        public InstallationService(IInstallationRepository installationRepository, IMapper mapper)
        {
            _installationRepository = installationRepository;
            _mapper = mapper;
        }

        #region Create installation
        public async Task<InstallationDto> AddInstallationAsync(InstallationDto installationDto, Guid companyId)
        {
            var installationModel = _mapper.Map<Installation>(installationDto);

            installationModel.CompanyId = companyId;

            var savedInstallation = await _installationRepository.AddAsync(installationModel);

            return _mapper.Map<InstallationDto>(savedInstallation);
        }

        #endregion

        #region Get Installation By Id
        public async Task<InstallationDto> GetInstallationAsync(Guid id, Guid companyId)
        {
            var installation = await _installationRepository.GetByIdAsync(id);

            if (installation == null || installation.CompanyId != companyId)
            {
                throw new InvalidOperationException(
                    "Installation not found or does not belong to your company."
                );
            }

            CompanyOwnershipGuard.Ensure(installation.CompanyId, companyId);

            return _mapper.Map<InstallationDto>(installation);
        }

        #endregion

        #region Get All Installations
        public async Task<List<InstallationDto>> GetAllInstallationAsync(Guid companyId)
        {
            var installations = await _installationRepository
                .GetAllByCompanyIdAsync(companyId);

            return _mapper.Map<List<InstallationDto>>(installations);
        }


        #endregion

        #region Update Installation
        public async Task<InstallationDto> UpdateInstallationAsync(
            InstallationDto installationDto,
            Guid companyId)
        {
            var installation = await _installationRepository.GetByIdAsync(installationDto.Id);

            if (installation == null)
                throw new InvalidOperationException("Installation not found.");

            CompanyOwnershipGuard.Ensure(installation.CompanyId, companyId);

            installation.Name = installationDto.Name;
            installation.Address = installationDto.Address;
            installation.Location = installationDto.Location;
            installation.City = installationDto.City;
            installation.Active = installationDto.Active;
            installation.UpdatedAt = DateTime.UtcNow;

            var updated = await _installationRepository.UpdateAsync(installation);

            return _mapper.Map<InstallationDto>(updated);
        }
        #endregion

        #region delete Installation
        public async Task<bool> DeleteInstallationAsync(Guid id, Guid companyId)
        {
            var installation = await _installationRepository.GetByIdAsync(id);

            if (installation == null || installation.CompanyId != companyId)
            {
                throw new InvalidOperationException(
                    "Installation not found or does not belong to your company."
                );
            }

            CompanyOwnershipGuard.Ensure(installation.CompanyId, companyId);

            await _installationRepository.DeleteAsync(id);

            return true;

        }

        #endregion

    }
}
