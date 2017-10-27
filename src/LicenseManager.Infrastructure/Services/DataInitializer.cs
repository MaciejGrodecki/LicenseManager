using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using NLog;

namespace LicenseManager.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
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
            Logger.Info("Seeding data");
            var tasks = new List<Task>();
            //Rooms
            tasks.Add(_roomService.AddAsync("B-01"));
            tasks.Add(_roomService.AddAsync("C-01"));
            //Users
            tasks.Add(_userService.AddAsync("Jan", "Kowalski"));
            tasks.Add(_userService.AddAsync("Anna", "Kowalska"));

            

            //LicenseTypes
            tasks.Add(_licenseTypeService.AddAsync("OEM"));
            tasks.Add(_licenseTypeService.AddAsync("BOX"));

            var licenseType1 = await _licenseTypeService.GetAsync("OEM");
            var licenseType2 = await _licenseTypeService.GetAsync("BOX");

            //Licenses
            tasks.Add(_licenseService.AddAsync("MS Office 2007 Pro", 100,
                licenseType1.LicenseTypeId, DateTime.UtcNow, "1A2B-3C4D-5E6F-7G58"));
            tasks.Add(_licenseService.AddAsync("MS Office 2010 Pro", 10,
                licenseType2.LicenseTypeId, DateTime.UtcNow, "1A2B-3C4D-5E6F-7G58"));

            var room1 = await _roomService.GetAsync("B-01");
            var room2 = await _roomService.GetAsync("C-01");

            //Computers
            tasks.Add(_computerService.AddAsync(Guid.NewGuid(), "US-IN/Z/2-W", "158.120.2.2", room2.RoomId));
            
            var computer = await _computerService.GetAsync("US-IN/Z/2-W");
            
            var user = await _userService.GetAsync("Jan", "Kowalski");
            ISet<Guid> usersIds = new HashSet<Guid>();
            usersIds.Add(user.UserId);
            
            tasks.Add(_computerService.AddUserToComputer(computer.ComputerId, usersIds));
            
            await Task.WhenAll(tasks);
        }
    }
}