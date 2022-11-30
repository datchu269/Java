using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldCup.Models;

namespace WorldCup.Data
{
    public class WorldCupContext : DbContext
    {
        public WorldCupContext (DbContextOptions<WorldCupContext> options)
            : base(options)
        {
        }

        public DbSet<WorldCup.Models.WorldCup> WorldCup { get; set; } = default!;
    }
}
