using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;
using LicenseManager.Infrastructure.DTO;

namespace LicenseManager.Infrastructure.Services
{
    public class LicenseTypeService : ILicenseTypeService
    {
        private readonly ILicenseTypeRepository _licenseTypeRepository;
        private readonly IMapper _mapper;
        
        public LicenseTypeService(ILicenseTypeRepository licenseTypeRepository, IMapper mapper)
        {
            _licenseTypeRepository = licenseTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LicenseTypeDto>> BrowseAsync()
        {
            var licenseTypes = await _licenseTypeRepository.BrowseAsync();

            return _mapper.Map<IEnumerable<LicenseTypeDto>>(licenseTypes);
        }

        public async Task<LicenseTypeDto> GetAsync(Guid licenseTypeId)
        {
            var licenseType = await _licenseTypeRepository.GetAsync(licenseTypeId);
            
            return _mapper.Map<LicenseTypeDto>(licenseType);
        }

        public async Task<LicenseTypeDto> GetAsync(string name)
        {
            var licenseType = await _licenseTypeRepository.GetAsync(name);

            return _mapper.Map<LicenseTypeDto>(licenseType);
        }

        public async Task AddAsync(string name)
        {
            var licenseType = await _licenseTypeRepository.GetAsync(name);
            if(licenseType != null)
            {
                throw new Exception($"License type with name: {name} already exist");
            }
            licenseType = new LicenseType(Guid.NewGuid(), name);
            await _licenseTypeRepository.AddAsync(licenseType);
        }

        public async Task RemoveAsync(Guid licenseTypeId)
        {
            var licenseType = await _licenseTypeRepository.GetAsync(licenseTypeId);
            if(licenseType == null)
            {
                throw new Exception($"License type with id: {licenseTypeId} doesn't exist");
            }

            await _licenseTypeRepository.RemoveAsync(licenseType);
        }

        public async Task UpdateAsync(Guid licenseTypeId, string name)
        {
            var licenseType = await _licenseTypeRepository.GetAsync(name);
            if(licenseType != null)
            {
                throw new Exception($"License type with name: {name} already exist");
            }

            licenseType = await _licenseTypeRepository.GetAsync(licenseTypeId);
            if(licenseType == null)
            {
                throw new Exception($"License type with id: {licenseTypeId} doesn't exist");
            }

            licenseType.SetName(name);
            
            await _licenseTypeRepository.UpdateAsync(licenseType);
        }
    }
}