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
        public DbSet<Scanner_CourseDBO> Scanner_Courses { get; set; }
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
            modelBuilder.Entity<CourseDBO>().HasIndex(x=>x.CourseName).IsUnique();
            modelBuilder.Entity<Client_CourseDBO>().HasKey(x => x.Id);
            modelBuilder.Entity<ScannerDBO>().HasKey(x => x.ScannerId);
            modelBuilder.Entity<Scanner_CourseDBO>().HasKey(x => x.Id);
            modelBuilder.Entity<AttendanceDBO>().HasKey(x => x.Id);
        }
    }
}
