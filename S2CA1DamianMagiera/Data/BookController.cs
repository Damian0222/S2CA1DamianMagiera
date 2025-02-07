using S2CA1DamianMagiera.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace S2CA1DamianMagiera.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}