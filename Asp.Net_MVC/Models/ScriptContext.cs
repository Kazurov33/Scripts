using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Asp.Net_MVC.Models
{
    public class ScriptContext : DbContext
    {
        public DbSet<Scene> Scenes { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Script> Scripts { get; set; }

        public ScriptContext(DbContextOptions<ScriptContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
