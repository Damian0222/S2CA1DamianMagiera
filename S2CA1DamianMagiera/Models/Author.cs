namespace S2CA1DamianMagiera.Models
{
   public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
  
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Bio { get; set; }
    }
}