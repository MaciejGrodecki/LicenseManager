using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;
using LicenseManager.Infrastructure.DTO;

namespace LicenseManager.Infrastructure.Services
{
    public class ComputerService : IComputerService
    {
        private readonly IComputerRepository _computerRepository;
        private readonly IMapper _mapper;

        public ComputerService(IComputerRepository computerRepository, IMapper mapper)
        {
            _computerRepository = computerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ComputerDto>> BrowseAsync()
        {
            var computers = await _computerRepository.BrowseAsync();

            return _mapper.Map<IEnumerable<ComputerDto>>(computers);
        }

        public async Task<ComputerDto> GetAsync(Guid computerId)
        {
            var computer = await _computerRepository.GetAsync(computerId);
            if(computer == null)
            {
                throw new Exception($"Computer with id: {computerId} doesn't exist");
            }

            return _mapper.Map<ComputerDto>(computer);
        }
        public async Task<ComputerDto> GetAsync(string inventoryNumber)
        {
            var computer = await _computerRepository.GetAsync(inventoryNumber);
            if(computer == null)
            {
                throw new Exception($"Computer with inventory number: {inventoryNumber} doesn't exist");
            }

            return _mapper.Map<ComputerDto>(computer);
        }
        public async Task AddAsync(string inventoryNumber, string ipAddress, Guid roomId)
        {
            var computer  = await _computerRepository.GetAsync(inventoryNumber);
            if(computer != null)
            {
                throw new Exception($"Computer with inventory number: {inventoryNumber} already exist");
            }
            computer = new Computer(inventoryNumber, ipAddress, roomId);
            await _computerRepository.AddAsync(computer);
        }

        public async Task RemoveAsync(Guid computerId)
        {
            var computer = await _computerRepository.GetAsync(computerId);
            if(computer == null)
            {
                throw new Exception($"Computer with id: {computerId} doesn't exist");
            }

            await _computerRepository.RemoveAsync(computer);

        }

        public async Task UpdateAsync(Guid computerId, string inventoryNumber, string ipAddress, 
            Guid roomId)
        {
            var computer = await _computerRepository.GetAsync(inventoryNumber);
            if(computer != null)
            {
                throw new Exception($"Computer with inventory number: {inventoryNumber} already exists");
            }

            computer = await _computerRepository.GetAsync(computerId);
            if(computer == null)
            {
                throw new Exception($"Computer with id: {computerId} doesn't exist");
            }
            
            computer.SetInventoryNumber(inventoryNumber);
            computer.SetInventoryNumber(ipAddress);

            await _computerRepository.UpdateAsync(computer);
        }
    }
}