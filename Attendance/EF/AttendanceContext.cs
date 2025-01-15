using Attendance.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance.EF
{
    public class AttendanceContext : DbContext
    {
        public AttendanceContext(DbContextOptions<AttendanceContext> options) : base(options)
        {
        }

        //Registered DB model in EntryContext file
        public DbSet<Card> Cards { get; set; }
        public DbSet<Client> Clients { get; set; }

        /*
         OnModelCreating mainly used to configured our EF model
         And insert master data if required
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Card>().HasKey(x => x.CardId);
            modelBuilder.Entity<Client>().HasKey(x => x.ClientId);

            modelBuilder.Entity<Card>().HasData(
                new Card
                {
                    CardId = -1,
                    ClientId = -1,
                    isActive = false
                });
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    ClientId = -1,
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Institutuion = "Instituion",
                    Email = "Email@email.com",
                    PhoneNumber = 1234567890
                });
        }
    }
}
