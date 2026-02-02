namespace ProjectLibrary.Entities
{
    //Professor pode usar os livros sem ser necessario a devolução apenas marcar o tempo de uso
    internal class Teacher : User 
    {
        public TimeSpan DayWithBook { get; set; } 
        public Teacher(string id, string name, int IDbook) : base(id, name, IDbook) { }
        public Teacher(string id, string name, int IDbook, DateTime InDate) : this(id, name, IDbook) 
        {
            DateOutBook = InDate;
            CalculateDay();
        }
        public double CalculateDay() //Dias que o professor está com o livro
        {
            DayWithBook += DateTime.Now.Subtract(DateOutBook); 
            return DayWithBook.TotalDays;
        }

        public override string ToString()
        {
            return $"{ID} - {Name} - Book: {IDBook} - Days:{DayWithBook}";
        }
    }
}
