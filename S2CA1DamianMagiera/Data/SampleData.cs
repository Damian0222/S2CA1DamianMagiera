using S2CA1DamianMagiera.Models;

namespace S2CA1DamianMagiera.Data
{
    public static class SeedData
    {
        public static void Initialize(BookContext context)
        {
            if (!context.Authors.Any()) 
            {
                context.Authors.AddRange(
                    new Author { Id = 1, Name = "J.K Rowling", Bio = "Harry Potter Books" },
                    new Author { Id = 2, Name = "George Martin", Bio = "Game of Thrones Books" }
                );
            }

            context.SaveChanges();
        }
    }
}