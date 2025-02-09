using S2CA1DamianMagiera.Models;

namespace S2CA1DamianMagiera.Data
{
    public static class SeedData
    {
        public static void Initialize(Library context)
        {
            if (context.Authors.Any()) return; 

            context.Authors.AddRange(
                new Author
                {
                    Id = 1,
                    Name = "J.K Rowling",
                    DateOfBirth = new DateTime(1965, 7, 31), 
                    Nationality = "British", 
                    Bio = "British author known for Harry Potter"
                },
                new Author
                {
                    Id = 2,
                    Name = "George Martin",
                    DateOfBirth = new DateTime(1948, 9, 20),
                    Nationality = "American",
                    Bio = "Known for Gamw of thrones"
                },
                new Author
                {
                    Id = 3,
                    Name = "J.R.R Tolkien",
                    DateOfBirth = new DateTime(1892, 1, 3),
                    Nationality = "British",
                    Bio = "Author of The Lord of the Rings and The Hobbit."
            
                }
                );
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book
                    {
                        Id = 1,
                        Title = "Harry Potter and the Sorcerer's Stone",
                        YearPublished = 1997,
                        Genre = "Fantasy",
                        PageCount = 300,
                        AuthorId = 1
                    },
                    new Book
                    {
                        Id = 2,
                        Title = "A Game of Thrones",
                        YearPublished = 1996,
                        Genre = "Fantasy",
                        PageCount = 700,
                        AuthorId = 2
                    }
                );
            }

            context.SaveChanges();
        }
    }
}