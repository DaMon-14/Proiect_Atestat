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
        public DbSet<Course> Courses { get; set; }
        public DbSet<Client_Course> Client_Courses { get; set; }
        public DbSet<Scanner> Scanners { get; set; }

        /*
         OnModelCreating mainly used to configured our EF model
         And insert master data if required
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<Card>().HasKey(x => x.CardId);
            modelBuilder.Entity<Client>().HasKey(x => x.ClientId);
            modelBuilder.Entity<Course>().HasKey(x => x.CourseId);
            modelBuilder.Entity<Client_Course>().HasKey(x => x.Id);
            modelBuilder.Entity<Scanner>().HasKey(x => x.ScannerId);

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
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    CourseId = -1,
                    CourseName = "CourseName",
                    InstitutionId = -1,
                    CourseDescription = "CourseDescription"
                });
            modelBuilder.Entity<Client_Course>().HasData(
                new Client_Course
                {
                    Id = -1,
                    ClientId = -1,
                    CourseId = -1,
                    Points = -1,
                    isEnrolled = false
                });
            modelBuilder.Entity<Scanner>().HasData(
                new Scanner
                {
                    ScannerId = -1,
                    ScannerName = "ScannerName",
                    isActive = false
                });
        }
    }
}
