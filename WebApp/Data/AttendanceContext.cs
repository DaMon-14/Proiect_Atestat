using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

    public class AttendanceContext : DbContext
    {
        public AttendanceContext (DbContextOptions<AttendanceContext> options)
            : base(options)
        {
        }

        public DbSet<WebApp.Models.Client> Client { get; set; } = default!;
    }
