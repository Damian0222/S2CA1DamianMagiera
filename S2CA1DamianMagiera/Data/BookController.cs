using S2CA1DamianMagiera.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace S2CA1DamianMagiera.Data
{
    public class BookstoreContext : DbContext
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options) : base(options) { }
        public DbSet<Author> Authors { get; set; }
         }
}