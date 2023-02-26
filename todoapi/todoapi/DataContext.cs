using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace todoapi
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<ToDo> ToDo => Set<ToDo>();
    }
}
