using LicenseManager.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace LicenseManager.Infrastructure.EntityFramework
{
    public class LicensesContext : DbContext
    {
        private SqlSettings _sqlSettings;

        public DbSet<Room> Rooms { get; set; }
        public DbSet<LicenseType> LicenseTypes { get; set; }
        public DbSet<User> Users {get; set;}
        public DbSet<License> Licenses {get; set;}
        public DbSet<Computer> Computers {get; set;}

        public LicensesContext(DbContextOptions<LicensesContext> options, IOptions<SqlSettings> sqlSettings) : base(options)
        {
            _sqlSettings = sqlSettings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_sqlSettings.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase();

                return;
            }
            optionsBuilder.UseSqlServer(_sqlSettings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var roomBuilder = modelBuilder.Entity<Room>();
            roomBuilder.HasKey(x => x.RoomId);

            var licenseTypeBuilder = modelBuilder.Entity<LicenseType>();
            licenseTypeBuilder.HasKey(x => x.LicenseTypeId);

            var userBuilder = modelBuilder.Entity<User>();
            userBuilder.HasKey(x => x.UserId);

            var licenseBuilder = modelBuilder.Entity<License>();
            licenseBuilder.HasKey(x => x.LicenseId);

            var computerBuilder = modelBuilder.Entity<Computer>();
            computerBuilder.HasKey(x => x.ComputerId);
        }
    }
}
