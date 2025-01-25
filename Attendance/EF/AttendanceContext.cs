using AttendanceAPI.EF.DBO;
using AttendanceAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceAPI.EF
{
    public class AttendanceContext : DbContext
    {
        public AttendanceContext(DbContextOptions<AttendanceContext> options) : base(options)
        {
        }

        //Registered DB model in EntryContext file
        public DbSet<CardDBO> Cards { get; set; }
        public DbSet<UserDBO> Users { get; set; } = default!;
        public DbSet<CourseDBO> Courses { get; set; }
        public DbSet<Client_CourseDBO> Client_Courses { get; set; }
        public DbSet<ScannerDBO> Scanners { get; set; }
        public DbSet<Scanner_Course> Scanner_Courses { get; set; }
        public DbSet<AttendanceDBO> Entries { get; set; }

        /*
         OnModelCreating mainly used to configured our EF model
         And insert master data if required
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Primary key
            modelBuilder.Entity<CardDBO>().HasKey(x => x.CardId);
            modelBuilder.Entity<UserDBO>().HasKey(x => x.ClientId);
            modelBuilder.Entity<UserDBO>().HasIndex(x => x.UserName).IsUnique();
            modelBuilder.Entity<CourseDBO>().HasKey(x => x.CourseId);
            modelBuilder.Entity<Client_CourseDBO>().HasKey(x => x.Id);
            modelBuilder.Entity<ScannerDBO>().HasKey(x => x.ScannerId);
            modelBuilder.Entity<Scanner_Course>().HasKey(x => x.Id);
            modelBuilder.Entity<AttendanceDBO>().HasKey(x => x.Id);

            modelBuilder.Entity<CardDBO>().HasData(
                new CardDBO
                {
                    CardId = -1,
                    ClientId = -1,
                    isActive = false
                });
            modelBuilder.Entity<UserDBO>().HasData(
                new UserDBO
                {
                    ClientId = -1,
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Institution = "Instituion",
                    Email = "Email@email.com",
                    PhoneNumber = 1234567890,
                    Password = "Password",
                    IsAdmin = false,
                    Salt = "Salt",
                    UserName = "UserName"
                });
            modelBuilder.Entity<CourseDBO>().HasData(
                new CourseDBO
                {
                    CourseId = -1,
                    CourseName = "CourseName",
                    InstitutionId = -1,
                    CourseDescription = "CourseDescription"
                });
            modelBuilder.Entity<Client_CourseDBO>().HasData(
                new Client_CourseDBO
                {
                    Id = -1,
                    ClientId = -1,
                    CourseId = -1,
                    Points = -1,
                    isEnrolled = false
                });
            modelBuilder.Entity<ScannerDBO>().HasData(
                new ScannerDBO
                {
                    ScannerId = -1,
                    ScannerName = "ScannerName",
                    isActive = false
                });
            modelBuilder.Entity<Scanner_Course>().HasData(
                new Scanner_Course
                {
                    Id = -1,
                    ScannerId = -1,
                    CourseId = -1,
                    isActive = false
                });
            modelBuilder.Entity<AttendanceDBO>().HasData(
                new AttendanceDBO
                {
                    Id = -1,
                    ClientId = -1,
                    CourseId = -1,
                    ScanTime = new DateTime(1,1,1,1,1,1,DateTimeKind.Utc)
                });
        }
    }
}
