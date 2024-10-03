using Microsoft.EntityFrameworkCore;
using Station.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
