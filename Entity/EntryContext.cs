using Microsoft.EntityFrameworkCore;
using Prezenta_API.Models;

namespace Prezenta_API.Entity
{
    public class EntryContext : DbContext
    {
        public EntryContext(DbContextOptions<EntryContext> options) :base (options) 
        { 
        }

        //Registered DB model in EntryContext file
        public DbSet<Entry> Entries { get; set; }

        /*
         OnModelCreating mainly used to configured our EF model
         And insert master data if required
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Entry>().HasKey(x => x.Id);

            //Inserting record
            modelBuilder.Entity<Entry>().HasData(
                new Entry
                {
                    Id = -1,
                    ScanTime = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                });
        }
    }
}
