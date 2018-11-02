using System;
using System.Data.Entity;
using ToDoList.Data.Models;

namespace ToDoList.Data
{
    public class EntryDbContext : DbContext
    {
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Entry> SubEntries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure One Entry -> Many SubEntries relationship
            modelBuilder.Entity<SubEntry>()
                 .HasRequired<Entry>(e => e.ParentEntry)
                 .WithMany(s => s.SubEntries)
                 .HasForeignKey<Guid>(s => s.ParentEntryId);

            modelBuilder.Entity<Entry>()
                .HasMany<SubEntry>(e => e.SubEntries)
                .WithRequired(s => s.ParentEntry)
                .WillCascadeOnDelete();

        }
    }
}
