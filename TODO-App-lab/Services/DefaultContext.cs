using Microsoft.EntityFrameworkCore;
using TODO_App_lab.Models;
using TODO_App_lab.Services.Interfaces;

namespace TODO_App_lab.Services
{
    public class DefaultContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        public string DbPath { get; }
        public DefaultContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "database.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>()
                .HasKey(c => c.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
