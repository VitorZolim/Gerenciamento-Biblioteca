namespace ProjectLibrary.Entities
{
    //Os alunos sempre que pega um livro devem devolver em um prazo de 7 dias
    internal class Student : User
    {
        public DateTime ReturnBook { get; set; }
        public Student(string id, string name, int IDbook) : base(id, name, IDbook)
        {
            ReturnBook = DateOutBook.AddDays(7);
        }

        public Student(string id, string name, int IDbook, DateTime InDate) : this(id, name, IDbook)
        {
            DateOutBook = InDate;
        }

        public bool Deadline() //Verifica se o livo está dentro do prazo
        {
            if (DateTime.Now <= ReturnBook) 
            {
                return true;
            }
            else { return false; }
        }

        public override string ToString()
        {
            return $"{ID} - {Name} - Book: {IDBook} - Term:{ReturnBook}";
        }
    }
}
