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
       Name = "J.K. Rowling",
       DateOfBirth = new DateTime(1965, 7, 31),
       Nationality = "British",
       Bio = "Author of the Harry Potter series",
       Books = new List<Book>
                {
                    new Book { Title = "Harry Potter and the Sorcerer's Stone", YearPublished = 1997, Genre = "Fantasy", PageCount = 300 },
                    new Book { Title = "Harry Potter and the Chamber of Secrets", YearPublished = 1998, Genre = "Fantasy", PageCount = 300 },
                    new Book { Title = "Harry Potter and the Prisoner of Azkaban", YearPublished = 1999, Genre = "Fantasy", PageCount = 400 },
                    new Book { Title = "Harry Potter and the Goblet of Fire", YearPublished = 2000, Genre = "Fantasy", PageCount = 600 },
                    new Book { Title = "Harry Potter and the Order of the Phoenix", YearPublished = 2003, Genre = "Fantasy", PageCount = 800 },
                    new Book { Title = "Harry Potter and the Half-Blood Prince", YearPublished = 2005, Genre = "Fantasy", PageCount = 600 },
                    new Book { Title = "Harry Potter and the Deathly Hallows", YearPublished = 2007, Genre = "Fantasy", PageCount = 600 }
                }
   },
            new Author
            {
                Id = 2,
                Name = "George R.R. Martin",
                DateOfBirth = new DateTime(1948, 9, 20),
                Nationality = "American",
                Bio = "Known for Game of Thrones",
                Books = new List<Book>
                {
                    new Book { Title = "A Game of Thrones", YearPublished = 1996, Genre = "Fantasy", PageCount = 700 },
                    new Book { Title = "A Clash of Kings", YearPublished = 1998, Genre = "Fantasy", PageCount = 800 },
                    new Book { Title = "A Storm of Swords", YearPublished = 2000, Genre = "Fantasy", PageCount = 1000 },
                    new Book { Title = "A Feast for Crows", YearPublished = 2005, Genre = "Fantasy", PageCount = 750 },
                    new Book { Title = "A Dance with Dragons", YearPublished = 2011, Genre = "Fantasy", PageCount = 1050 }
                }
            },
            new Author
            {
                Id = 3,
                Name = "J.R.R. Tolkien",
                DateOfBirth = new DateTime(1892, 1, 3),
                Nationality = "British",
                Bio = "Author of The Lord of the Rings and The Hobbit.",
                Books = new List<Book>
                {
                    new Book { Title = "The Hobbit", YearPublished = 1937, Genre = "Fantasy", PageCount = 300 },
                    new Book { Title = "The Fellowship of the Ring", YearPublished = 1954, Genre = "Fantasy", PageCount = 600 },
                    new Book { Title = "The Two Towers", YearPublished = 1954, Genre = "Fantasy", PageCount = 500 },
                    new Book { Title = "The Return of the King", YearPublished = 1955, Genre = "Fantasy", PageCount = 410 },
                    new Book { Title = "The Silmarillion", YearPublished = 1977, Genre = "Fantasy", PageCount = 600 }
                }
            },
            new Author
            {
                Id = 4,
                Name = "Stephen King",
                DateOfBirth = new DateTime(1947, 9, 21),
                Nationality = "American",
                Bio = "Master of horror fiction.",
                Books = new List<Book>
                {
                    new Book { Title = "The Shining", YearPublished = 1977, Genre = "Horror", PageCount = 447 },
                    new Book { Title = "It", YearPublished = 1986, Genre = "Horror", PageCount = 1138 },
                    new Book { Title = "Misery", YearPublished = 1987, Genre = "Horror", PageCount = 370 },
                    new Book { Title = "The Green Mile", YearPublished = 1996, Genre = "Drama", PageCount = 432 },
                    new Book { Title = "Doctor Sleep", YearPublished = 2013, Genre = "Horror", PageCount = 531 }
                }
            },
            new Author
            {
                Id = 5,
                Name = "Brandon Sanderson",
                DateOfBirth = new DateTime(1975, 12, 19),
                Nationality = "American",
                Bio = "Known for the Mistborn series and The Stormlight Archive.",
                Books = new List<Book>
                {
                    new Book { Title = "The Final Empire", YearPublished = 2006, Genre = "Fantasy", PageCount = 541 },
                    new Book { Title = "The Well of Ascension", YearPublished = 2007, Genre = "Fantasy", PageCount = 590 },
                    new Book { Title = "The Hero of Ages", YearPublished = 2008, Genre = "Fantasy", PageCount = 572 },
                    new Book { Title = "The Way of Kings", YearPublished = 2010, Genre = "Fantasy", PageCount = 1007 },
                    new Book { Title = "Words of Radiance", YearPublished = 2014, Genre = "Fantasy", PageCount = 1088 }
                }
            }
        );

            context.SaveChanges();
        }
    }
}