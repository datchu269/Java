using Microsoft.EntityFrameworkCore;
using NSC_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSC_project.Data
{
    public class NSC_projectContext : DbContext
    {
        public NSC_projectContext(DbContextOptions<NSC_projectContext> options)
            : base(options)
        {
        }

        public DbSet<NSC_project.Models.User> User { get; set; } = default!;

        public DbSet<NSC_project.Models.Movie> Movie { get; set; } = default!;

        public DbSet<NSC_project.Models.Theater> Theater { get; set; } = default!;

        public DbSet<NSC_project.Models.Auditorium> Auditorium { get; set; } = default!;

        public DbSet<NSC_project.Models.Screening> Screening { get; set; } = default!;

        public DbSet<NSC_project.Models.Seat> Seat { get; set; } = default!;

        public DbSet<NSC_project.Models.Reservetion> Reservetion { get; set; } = default!;

        public DbSet<NSC_project.Models.ReservedSeat> ReservedSeat { get; set; } = default!;
    }
}
