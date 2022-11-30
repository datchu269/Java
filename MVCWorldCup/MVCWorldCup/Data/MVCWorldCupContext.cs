using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCWorldCup.Models;

namespace MVCWorldCup.Data
{
    public class MVCWorldCupContext : DbContext
    {
        public MVCWorldCupContext (DbContextOptions<MVCWorldCupContext> options)
            : base(options)
        {
        }

        public DbSet<MVCWorldCup.Models.Group> Group { get; set; } = default!;

        public DbSet<MVCWorldCup.Models.Team> Team { get; set; } = default!;
    }
}
