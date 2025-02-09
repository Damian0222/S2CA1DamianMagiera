namespace S2CA1DamianMagiera.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearPublished { get; set; } 
        public string Genre { get; set; }
        public int PageCount { get; set; } 
        public int AuthorId { get; set; } 
    }
}
