using BooksWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BooksWebAPI.DBUtility
{
    public class BooksAPIDbContext : DbContext
    {
        public BooksAPIDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<Member> Members { get; set; }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to Many
            modelBuilder.Entity<Book>()
                .HasOne<Class>(b => b.Class)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.ClassId);

            modelBuilder.Entity<Book>()
                .HasOne<Code>(b => b.Code)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CodeId);

            modelBuilder.Entity<Book>()
                .HasOne<Member>(b => b.Member)
                .WithMany(m => m.Books)
                .HasForeignKey(b => b.MemberId);


            //Delete
            modelBuilder.Entity<Class>()
                .HasMany<Book>(c => c.Books)
                .WithOne(b => b.Class)
                .HasForeignKey(b => b.ClassId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Code>()
                .HasMany<Book>(c => c.Books)
                .WithOne(b => b.Code)
                .HasForeignKey(b => b.CodeId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Member>()
                .HasMany<Book>(m => m.Books)
                .WithOne(b => b.Member)
                .HasForeignKey(b => b.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
