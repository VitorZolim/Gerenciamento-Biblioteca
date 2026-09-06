namespace Library.Domain.Entities
{
    public class UserBook
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }

        public DateTime DateOutBook { get; set; }
        public DateTime ReturnBook { get; set; }

        public UserBook()
        {
            DateOutBook = DateTime.UtcNow;
            ReturnBook = DateOutBook.AddDays(7);
        }
    }
}
