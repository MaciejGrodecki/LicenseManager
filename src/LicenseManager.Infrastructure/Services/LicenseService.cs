using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;
using LicenseManager.Infrastructure.DTO;
using NLog;

namespace LicenseManager.Infrastructure.Services
{
    public class LicenseService : ILicenseService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ILicenseRepository _licenseRepository;
        private readonly IComputerRepository _computerRepository;
        private readonly IMapper _mapper;

        public LicenseService(ILicenseRepository licenseRepository, IComputerRepository computerRepository, IMapper mapper)
        {
            _licenseRepository = licenseRepository;
            _computerRepository = computerRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<LicenseDto>> BrowseAsync()
        {
            Logger.Info("Getting all licenses");
            var licenses = await _licenseRepository.BrowseAsync();
            
            return _mapper.Map<IEnumerable<LicenseDto>>(licenses);
        }
        public async Task<IEnumerable<LicenseDto>> BrowseAsync(string name)
        {
            Logger.Info("Getting all licenses");
            var licenses = await _licenseRepository.BrowseAsync(name);
            return _mapper.Map<IEnumerable<LicenseDto>>(licenses);
        }
        public async Task<LicenseDto> GetAsync(Guid licenseId)
        {
            Logger.Info("Getting single license");
            var license = await _licenseRepository.GetAsync(licenseId);
            NotNull(license);
            
            return _mapper.Map<LicenseDto>(license);
        }      
        public async Task AddAsync(string name, int count, Guid licenseTypeId,
             DateTime buyDate, string serialNumber)
        {
            Logger.Info("Adding license");
            var license = new License(name, count, licenseTypeId, buyDate, serialNumber);

            await  _licenseRepository.AddAsync(license);
        }
        public async Task RemoveAsync(Guid licenseId)
        {
            Logger.Info("Removing license");
            var license = await _licenseRepository.GetAsync(licenseId);
            NotNull(license);
            
            await _licenseRepository.RemoveAsync(license);
        }

        public async Task UpdateAsync(Guid licenseId, string name, int count, Guid licenseTypeId,
             DateTime buyDate, string serialNumber)
        {
            Logger.Info("Updating license");
            var license = await _licenseRepository.GetAsync(licenseId);
            NotNull(license);
            
            license.SetName(name);
            license.SetCount(count);
            license.SetLicenseType(licenseTypeId);
            license.SetBuyDate(buyDate);
            license.SetSerialNumber(serialNumber);
            await _licenseRepository.UpdateAsync(license);
        }

        public async Task AddComputer(Guid licenseId, ISet<Guid> computerIds)
        {
            Logger.Info("Adding computer to license");
            var license = await _licenseRepository.GetAsync(licenseId);
            NotNull(license);
            
            license.Computers.Clear();
            foreach(Guid computerId in computerIds)
            {
                var computer = await _computerRepository.GetAsync(computerId);
                license.AddComputer(computer);
            }
            await _licenseRepository.UpdateAsync(license);

        }


        private void NotNull(License license)
        {
            if(license == null)
            {
                Logger.Error("License doesn't exist");
                throw new Exception("License doesn't exist");              
            }
        }
    }
}