namespace Library.Domain.Entities
{
    public class Book 
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int Quantity { get; set; } //coluna calculada
        public Category Category { get; set; }

        public bool Available()
        {
            return Quantity > 0;
        }
    }
}
