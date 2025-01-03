using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using Station.Core.Entities;
using Station.Core.Entities.Identities;
using Station.Core.Enums;
using Station.Web.Host.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt;
using System.Data;
using Station.Core.Configurations;

namespace Station.Core
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<ChargeStation> ChargeStations { get; set; }
        public DbSet<StationEvent> StationEvents { get; set; }
        public DbSet<StationEventType> StationEventTypes { get; set; }
        public DbSet<StationStatus> StationStatuses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionEvent> SessionEvents { get; set; }
        public DbSet<SessionEventType> SessionEventTypes { get; set; }
        public DbSet<SessionStatus> SessionStatuses { get; set; }


        public DbSet<Connector> Connectors { get; set; }
        public DbSet<ConnectorEvent> ConnectorEvents { get; set; }
        public DbSet<ConnectorEventType> ConnectorEventTypes { get; set; }
        public DbSet<ConnectorStatus> ConnectorStatuses { get; set; }
        public DbSet<ConnectorType> ConnectorTypes { get; set; }
        public DbSet<ConnectorUiStatus> ConnectorUiStatuses { get; set; }

        //RolePermission
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<PermissionAction> PermissionActions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<PermissionCategory> PermissionCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var hashadPasword = BCrypt.Net.BCrypt.EnhancedHashPassword("123Pa$$word!");
            builder.Entity<User>().HasData(new User
            {
                Id = 1,
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                IsActive = true,
                FirstName = "admin",
                LastName = "admin",
                HashPasword = hashadPasword,
            });

            IEnumerable<Role> roles = Enum.GetValues<RoleEnum>().Select(r => new Role
            {
                Id = (int)r,
                Name = r.GetDisplayValue(),
                Description = r.GetDisplayValue(),
            });
            builder.Entity<Role>().HasData(roles);

            builder.ApplyConfiguration(new PermissionCategoriesConfiguration());
            builder.ApplyConfiguration(new PermissionActionsConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());


            //IEnumerable<PermissionCategory> permissionCategories = Enum.GetValues<PermissionCategoryEnum>().Select(r => new PermissionCategory
            //{
            //    Id = (int)r,
            //    Name = r.GetDisplayValue(),
            //    Description = r.GetDisplayValue(),
            //});


        }
    }
}
