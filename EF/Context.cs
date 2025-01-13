using Microsoft.EntityFrameworkCore;
using Prezenta_API.Models;

namespace Prezenta_API.EF
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) :base (options) 
        { 
        }

        //Registered DB model in EntryContext file
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Mapper> Mappers { get; set; }
        public DbSet<User> Users { get; set; }

        /*
         OnModelCreating mainly used to configured our EF model
         And insert master data if required
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Entry>().HasKey(x => x.Id);
            modelBuilder.Entity<Mapper>().HasKey(x => x.UserCode);
            modelBuilder.Entity<User>().HasKey(x => x.UserId);

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
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = -1,
                    FirstName = "First",
                    LastName = "Last",
                    Email = "test@mail.com",
                    PhoneNumber = "1234567890",
                });
        }
    }
}
