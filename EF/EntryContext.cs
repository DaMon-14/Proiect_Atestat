using Microsoft.EntityFrameworkCore;
using Prezenta_API.Models;

namespace Prezenta_API.EF
{
    public class EntryContext : DbContext
    {
        public EntryContext(DbContextOptions<EntryContext> options) :base (options) 
        { 
        }

        //Registered DB model in EntryContext file
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Mapper> Mappers { get; set; }

        /*
         OnModelCreating mainly used to configured our EF model
         And insert master data if required
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Entry>().HasKey(x => x.Id);
            modelBuilder.Entity<Mapper>().HasKey(x => x.UserCode);

            //Inserting record
            modelBuilder.Entity<Entry>().HasData(
                new Entry
                {
                    Id = -1,
                    UserCode = -1,
                    ScanTime = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    ScannerId = -1
                });
            modelBuilder.Entity<Mapper>().HasData(
                new Mapper
                {
                    UserId = -1,
                    UserCode = -1,
                    isActive = false
                });
        }
    }
}
