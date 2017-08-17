using System;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;

namespace LicenseManager.Infrastructure.Extensions {
    public static class RepositoryExtensions {
        public static async Task<Room> GetOrFailAsync (this IRoomRepository repository,
            Guid roomId) {
            var room = await repository.GetAsync (roomId);
            if (room == null) {
                throw new Exception ($"Room with id: {roomId} does not exist");
            }
            return room;
        }

        public static async Task<Room> GetOrFailAsync (this IRoomRepository repository,
            string name) {
            var room = await repository.GetAsync (name);
            if (room == null) {
                throw new Exception ($"Room with name: {name} does not exist");
            }
            return room;
        }

        public static async Task<LicenseType> GetOrFailAsync (this ILicenseTypeRepository repository,
            Guid licenseTypeId) {
                var licenseType = await repository.GetAsync(licenseTypeId);
                if(licenseType == null)
                {
                    throw new Exception($"License type with id: {licenseTypeId} does not exist");
                }
                return licenseType;
        }

        public static async Task<LicenseType> GetOrFailAsync (this ILicenseTypeRepository repository,
            string name) {
                var licenseType = await repository.GetAsync(name);
                if(licenseType == null)
                {
                    throw new Exception($"License type with name: {name} does not exist");
                }
                return licenseType;
        }

        public static async Task<User> GetOrFailAsync (this IUserRepository repository,
            Guid userId) {
                var user = await repository.GetAsync(userId);
                if(user == null)
                {
                    throw new Exception($"User with id: {userId} does not exist");
                }
                return user;
        }

        public static async Task<User> GetOrFailAsync (this IUserRepository repository,
            string name, string surname) {
                var user = await repository.GetAsync(name, surname);
                if(user == null)
                {
                    throw new Exception($"User with name: {name} and surname: {surname} does not exist");
                }
                return user;
        }

        public static async Task<License> GetOrFailAsync (this ILicenseRepository repository,
            Guid licenseId) {
                var license = await repository.GetAsync(licenseId);
                if(license == null)
                {
                    throw new Exception($"License with id: {licenseId} does not exist");
                }
                return license;
        }

        public static async Task<Computer> GetOrFailAsync (this IComputerRepository repository,
            Guid computerId) {
                var computer = await repository.GetAsync(computerId);
                if(computer == null)
                {
                    throw new Exception($"Computer with id: {computer} does not exist");
                }
                return computer;
        }

        public static async Task<Computer> GetOrFailAsync (this IComputerRepository repository,
            string inventoryNumber) {
                var computer = await repository.GetAsync(inventoryNumber);
                if(computer == null)
                {
                    throw new Exception($"Computer with inventory number: {inventoryNumber} does not exist");
                }
                return computer;
        }
        
    }
}