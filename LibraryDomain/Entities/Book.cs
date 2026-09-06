namespace Library.Domain.Entities
{
    public class Book 
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int Quantity { get; set; }
        public string Author { get; set; }
        public Category Category { get; set; }
        public ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
    }
}
