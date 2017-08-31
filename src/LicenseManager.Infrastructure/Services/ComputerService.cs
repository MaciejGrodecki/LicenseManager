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
        private readonly IMapper _mapper;

        public ComputerService(IComputerRepository computerRepository, IMapper mapper)
        {
            _computerRepository = computerRepository;
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
            if(computer == null)
            {
                throw new Exception($"Computer with id: {computerId} doesn't exist");
            }

            return _mapper.Map<ComputerDto>(computer);
        }
        public async Task<ComputerDto> GetAsync(string inventoryNumber)
        {
            Logger.Info("Getting single computer");
            var computer = await _computerRepository.GetAsync(inventoryNumber);
            if(computer == null)
            {
                throw new Exception($"Computer with inventory number: {inventoryNumber} doesn't exist");
            }

            return _mapper.Map<ComputerDto>(computer);
        }
        public async Task AddAsync(Guid computerId, string inventoryNumber, string ipAddress, Guid roomId)
        {
            Logger.Info("Adding computer");
            var computer  = await _computerRepository.GetAsync(inventoryNumber);
            if(computer != null)
            {
                throw new Exception($"Computer with inventory number: {inventoryNumber} already exist");
            }
            computer = new Computer(computerId, inventoryNumber, ipAddress, roomId);
            await _computerRepository.AddAsync(computer);
        }

        public async Task RemoveAsync(Guid computerId)
        {
            Logger.Info("Removing computer");
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
            Logger.Info("Updating computer");
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
            computer.AssignRoomToComputer(roomId);

            await _computerRepository.UpdateAsync(computer);
        }

        public async Task AddUserToComputer(Guid computerId, UserDto userDto)
        {
            Logger.Info("Assign user to computer");
            var computer = await _computerRepository.GetAsync(computerId);
            if(computer == null)
            {
                throw new Exception($"Computer with {computerId} doesn't exist");
            }
            var user = new User(userDto.Name, userDto.Surname);

            if(computer.Users.Contains(user))
            {
                throw new Exception ($"User already assigned to computer");
            }
            computer.Users.Add(user);
        }
    }
}