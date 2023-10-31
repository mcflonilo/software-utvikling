using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Student> students => Set<Student>();
        public DbSet<Education> education => Set<Education>();
        public DbSet<Teacher> teacher => Set<Teacher>();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data source = resources/school.db");
        }

    }
}
