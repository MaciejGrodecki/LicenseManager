using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LicenseManager.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IComputerService _computerService;
        private readonly IRoomService _roomService;
        private readonly ILicenseTypeService _licenseTypeService;
        private readonly ILicenseService _licenseService;
        private readonly IUserService _userService;

        public DataInitializer(IComputerService computerService, IRoomService roomService, 
           ILicenseTypeService licenseTypeService, ILicenseService licenseService, IUserService userService)
        {
            _computerService = computerService;
            _roomService = roomService;
            _licenseTypeService = licenseTypeService;
            _licenseService = licenseService;
            _userService = userService;
        }
        public async Task SeedAsync()
        {
            var tasks = new List<Task>();
            //Rooms
            tasks.Add(_roomService.AddAsync("B-01"));
            tasks.Add(_roomService.AddAsync("C-01"));
            //LicenseTypes
            tasks.Add(_licenseTypeService.AddAsync("OEM"));
            tasks.Add(_licenseTypeService.AddAsync("BOX"));
            //Licenses
            tasks.Add(_licenseService.AddAsync("MS Office 2007 Pro", 100,
                Guid.NewGuid(), DateTime.UtcNow));
            tasks.Add(_licenseService.AddAsync("MS Office 2010 Pro", 10,
                Guid.NewGuid(), DateTime.UtcNow));
            //Computers
            tasks.Add(_computerService.AddAsync(Guid.NewGuid(), "US-IN/Z/1-W", "10.11.2.100", Guid.NewGuid()));
            tasks.Add(_computerService.AddAsync(Guid.NewGuid(), "US-IN/Z/1-W", "10.11.2.100", Guid.NewGuid()));
            //Users
            tasks.Add(_userService.AddAsync("Jan", "Kowalski"));

            await Task.WhenAll(tasks);
        }
    }
}