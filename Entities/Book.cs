using ProjectLibrary.Entities.Enum;

namespace ProjectLibrary.Entities
{
    internal class Book : IComparable
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }

        public Book(int iD, string name, Category category, int quantity)
        {
            Name = name;
            ID = iD;
            Quantity = quantity;
            Category = category;
        }

        public bool Available()
        {
            return Quantity > 0;
        }

        public override string ToString()
        {
            return $"{ID} - {Name} - {Category} | Quantity: [{Quantity}] ";
        }

        public int CompareTo(object obj) //Talvez não seja necessario
        {
            if(!(obj is Book))
            {
                throw new ArgumentException("[Comparing error]");
            }
            Book other = (Book)obj;
            return ID.CompareTo(other.ID);
        }
    }
}
