using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace LMSproj.Models
{
    public class LibraryContext:DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        
        // DbSets for each entity
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookRequest> BookRequests { get; set; }
        public DbSet<BookIssue> BookIssues { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<UserActivityLog> UserActivityLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {


            modelBuilder.Entity<BookRequest>()

                .HasOne(br => br.Book)

                .WithMany(b => b.BookRequests)

                .HasForeignKey(br => br.BookId)

                .OnDelete(DeleteBehavior.Cascade);  




            modelBuilder.Entity<BookIssue>()

                .HasOne(bi => bi.Book)

                .WithMany(b => b.BookIssues)

                .HasForeignKey(bi => bi.BookId)

                .OnDelete(DeleteBehavior.Restrict);  




            modelBuilder.Entity<Fine>()

               

                .HasOne(f => f.BookIssue)

                .WithOne(bi => bi.Fine)

                .HasForeignKey<Fine>(f => f.IssueId)

                .OnDelete(DeleteBehavior.Restrict);  
            

        }
    }
}
