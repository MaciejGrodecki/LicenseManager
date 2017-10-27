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
    public class ComputerService : IComputerService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IComputerRepository _computerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILicenseRepository _licenseRepository;
        private readonly IMapper _mapper;

        public ComputerService(IComputerRepository computerRepository, IUserRepository userRepository, ILicenseRepository licenseRepository,
            IMapper mapper)
        {
            _computerRepository = computerRepository;
            _userRepository = userRepository;
            _licenseRepository = licenseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ComputerDto>> BrowseAsync()
        {
            Logger.Info("Getting all computers");
            var computers = await _computerRepository.BrowseAsync();

            return _mapper.Map<IEnumerable<ComputerDto>>(computers);
        }

        public async Task<ComputerDto> GetAsync(Guid computerId)
        {
            Logger.Info("Getting single computer");
            var computer = await _computerRepository.GetAsync(computerId);
            NullCheck(computer);

            return _mapper.Map<ComputerDto>(computer);
        }
        public async Task<ComputerDto> GetAsync(string inventoryNumber)
        {
            Logger.Info("Getting single computer");
            var computer = await _computerRepository.GetAsync(inventoryNumber);
            NullCheck(computer);

            return _mapper.Map<ComputerDto>(computer);
        }
        public async Task AddAsync(Guid computerId, string inventoryNumber, string ipAddress, Guid roomId)
        {
            Logger.Info("Adding computer");
            var computer  = await _computerRepository.GetAsync(inventoryNumber);
            NotNullCheck(computer);

            computer = new Computer(computerId, inventoryNumber, ipAddress, roomId);
            await _computerRepository.AddAsync(computer);
        }

        public async Task RemoveAsync(Guid computerId)
        {
            Logger.Info("Removing computer");
            var computer = await _computerRepository.GetAsync(computerId);
            NullCheck(computer);

            await _computerRepository.RemoveAsync(computer);

        }

        public async Task UpdateAsync(Guid computerId, string inventoryNumber, string ipAddress, 
            Guid roomId, ISet<Guid> userIds)
        {
            Logger.Info("Updating computer");

            var computer = await _computerRepository.GetAsync(computerId);
            NullCheck(computer);
            
            computer.SetInventoryNumber(inventoryNumber);
            computer.SetIpAddress(ipAddress);
            computer.SetRoom(roomId);

            computer.Users.Clear();
            foreach(Guid userId in userIds)
            {
                var user = await _userRepository.GetAsync(userId);
                computer.AddUser(user);
            }

            await _computerRepository.UpdateAsync(computer);
        }

        public async Task AddUserToComputer(Guid computerId, ISet<Guid> userIds)
        {
            Logger.Info("Assign user to computer");
            var computer = await _computerRepository.GetAsync(computerId);
            NullCheck(computer);

            foreach(Guid userId in userIds)
            {
                var user = await _userRepository.GetAsync(userId);
                computer.Users.Add(user);
            }
            await _computerRepository.UpdateAsync(computer);
        }

        private void NullCheck(Computer computer)
        {
            if(computer == null)
            {
                throw new Exception($"Computer doesn't exist");
            }
        }

        private void NotNullCheck(Computer computer)
        {
            if(computer != null)
            {
                throw new Exception($"Computer already exist");
            }
        }
    }
}