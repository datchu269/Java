using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HRM.Model;

namespace HRM.Data
{
    public class HRMContext : DbContext
    {
        public HRMContext (DbContextOptions<HRMContext> options)
            : base(options)
        {
        }

        public DbSet<HRM.Model.Employee> Employee { get; set; } = default!;
    }
}
