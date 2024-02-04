using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Design;
using System;
using SQLitePCL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace uni_tech_test.Model
{
    public class DataBaseContext : DbContext
    {
        public DbSet<CopyFile> CopyFiles { get; set; }

        public DataBaseContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dir = Directory.GetCurrentDirectory();
            var path = Directory.GetParent(dir).Parent.Parent;
            optionsBuilder.UseSqlite($"Filename={path}\\Model\\uni_tech.db");
        }
    }
}
