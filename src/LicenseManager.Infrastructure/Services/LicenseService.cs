using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;
using LicenseManager.Infrastructure.DTO;

namespace LicenseManager.Infrastructure.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly ILicenseRepository _licenseRepository;
        private readonly IMapper _mapper;

        public LicenseService(ILicenseRepository licenseRepository, IMapper mapper)
        {
            _licenseRepository = licenseRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<LicenseDto>> BrowseAsync()
        {
            var licenses = await _licenseRepository.BrowseAsync();
            
            return _mapper.Map<IEnumerable<LicenseDto>>(licenses);
        }
        public async Task<IEnumerable<LicenseDto>> BrowseAsync(string name)
        {
            var licenses = await _licenseRepository.BrowseAsync(name);
            return _mapper.Map<IEnumerable<LicenseDto>>(licenses);
        }
        public async Task<LicenseDto> GetAsync(Guid licenseId)
        {
            var license = await _licenseRepository.GetAsync(licenseId);
            if(license == null)
            {
                throw new Exception($"License with id: {licenseId} doesn't exist");
            }

            return _mapper.Map<LicenseDto>(license);
        }      
        public async Task AddAsync(string name, int count, Guid licenseTypeId,
             DateTime buyDate)
        {
            var license = new License(name, count, licenseTypeId, buyDate);

            await  _licenseRepository.AddAsync(license);
        }
        public async Task RemoveAsync(Guid licenseId)
        {
            var license = await _licenseRepository.GetAsync(licenseId);
            if(license == null)
            {
                throw new Exception($"License with id: {licenseId} doesn't exist");
            }

            await _licenseRepository.RemoveAsync(license);
        }

        public async Task UpdateAsync(Guid licenseId, string name, int count, Guid licenseTypeId,
             DateTime buyDate)
        {
            var license = await _licenseRepository.GetAsync(licenseId);
            if(license == null)
            {
                throw new Exception($"License with id: {licenseId} doesn't exist");
            }
            license.SetName(name);
            license.SetCount(count);
            license.SetLicenseType(licenseTypeId);
            license.SetBuyDate(buyDate);
            await _licenseRepository.UpdateAsync(license);
        }
    }
}