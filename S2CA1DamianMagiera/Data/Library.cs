
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using S2CA1DamianMagiera.Models;

namespace S2CA1DamianMagiera.Data
{
    public class Library : DbContext
    {
        public Library(DbContextOptions<Library> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
    
    public DbSet<Book> Books { get; set; }
    }
}
