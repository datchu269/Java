using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todolist2.Model;

namespace todolist2.Data
{
    public class todolist2Context : DbContext
    {
        public todolist2Context (DbContextOptions<todolist2Context> options)
            : base(options)
        {
        }

        public DbSet<todolist2.Model.ToDo> ToDo { get; set; } = default!;
    }
}
