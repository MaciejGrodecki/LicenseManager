using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;
using LicenseManager.Infrastructure.DTO;

namespace LicenseManager.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.BrowseAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if(user == null)
            {
                throw new Exception($"User type with id: {userId} doesn't exist");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetAsync(string name, string surname)
        {
            var user = await _userRepository.GetAsync(name, surname);
            if(user == null)
            {
                throw new Exception($"User type with name: {name} and surname: {surname} doesn't exist");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task AddAsync(string name, string surname)
        {
            var user = await _userRepository.GetAsync(name,surname);
            if(user != null)
            {
                throw new Exception($"User with name: {name} and username: {surname} already exist");
            }
            user = new User(name, surname);
            await _userRepository.AddAsync(user);
        }

        public async Task RemoveAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if(user == null)
            {
                throw new Exception($"User type with id: {userId} doesn't exist");
            }

            await _userRepository.RemoveAsync(user);
        }

        public async Task UpdateAsync(Guid userId, string name, string surname)
        {
            var user = await _userRepository.GetAsync(name, surname);
            if(user != null)
            {
                throw new Exception($"User with name: {name} and {surname} already exists");
            }

            user = await _userRepository.GetAsync(userId);
            if(user == null)
            {
                throw new Exception($"User type with id: {userId} doesn't exist");
            }

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