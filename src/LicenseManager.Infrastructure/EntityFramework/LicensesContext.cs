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

        public LicensesContext(DbContextOptions<LicensesContext> options, IOptions<SqlSettings> sqlSettings) : base(options)
        {
            _sqlSettings = sqlSettings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(_sqlSettings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var roomBuilder = modelBuilder.Entity<Room>();
            roomBuilder.HasKey(x => x.RoomId);
        }
    }
}
