using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MCVManagerEmployees.Models;

namespace MCVManagerEmployees.Data
{
    public class MCVManagerEmployeesContext : DbContext
    {
        public MCVManagerEmployeesContext (DbContextOptions<MCVManagerEmployeesContext> options)
            : base(options)
        {
        }

        public DbSet<MCVManagerEmployees.Models.Employee> Employee { get; set; } = default!;
    }
}
