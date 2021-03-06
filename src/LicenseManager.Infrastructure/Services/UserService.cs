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
    public class UserService : IUserService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            Logger.Info("Getting All users");
            var users = await _userRepository.BrowseAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetAsync(Guid userId)
        {
            Logger.Info("Getting single user");
            var user = await _userRepository.GetAsync(userId);
            //Exception if user is null
            NullCheck.IsNull(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetAsync(string name, string surname)
        {
            Logger.Info("Getting single user");
            var user = await _userRepository.GetAsync(name, surname);
            //Exception if user is null
            NullCheck.IsNull(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task AddAsync(string name, string surname)
        {
            Logger.Info("Adding user");
            var user = await _userRepository.GetAsync(name,surname);
            //Exception if user is not null
            NullCheck.IsNotNull(user);

            user = new User(Guid.NewGuid(), name, surname);
            await _userRepository.AddAsync(user);
        }

        public async Task RemoveAsync(Guid userId)
        {
            Logger.Info("Removing user");
            var user = await _userRepository.GetAsync(userId);
            //Exception if user is null
            NullCheck.IsNull(user);

            await _userRepository.RemoveAsync(user);
        }

        public async Task UpdateAsync(Guid userId, string name, string surname)
        {
            Logger.Info("Updating user");
            var user = await _userRepository.GetAsync(name, surname);
            //Exception if user is not null
            NullCheck.IsNotNull(user);

            user = await _userRepository.GetAsync(userId);
            //Exception if user is null
            NullCheck.IsNull(user);

            if(user.Name != name && name != null)
            {
                user.SetName(name);
            }
            if(user.Surname != surname && surname != null)
            {
                user.SetSurname(surname);
            }
            
            await _userRepository.UpdateAsync(user);
        }
    }
}