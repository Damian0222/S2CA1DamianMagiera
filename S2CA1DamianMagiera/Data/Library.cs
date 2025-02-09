
using System.Collections.Generic;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using S2CA1DamianMagiera.Models;

namespace S2CA1DamianMagiera.Data
{
    //Connects code to database
    public class Library : DbContext
    {
        public Library(DbContextOptions<Library> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)   // A book has one author
                .WithMany(a => a.Books)  // An author has many books
                .HasForeignKey(b => b.AuthorId); // Foreign key
        }
    }
}