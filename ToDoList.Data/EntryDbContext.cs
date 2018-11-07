using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using ToDoList.Data.Models;

namespace ToDoList.Data
{
    public class EntryDbContext : DbContext
    {
        public DbSet<Entry> Entries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>()
                .HasMany(e => e.Steps)
                .WithOptional()
                .HasForeignKey(s => s.EntryId);
            modelBuilder.Entity<Step>()
                .HasKey(s => new { s.Id, s.EntryId })
                .Property(s => s.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
