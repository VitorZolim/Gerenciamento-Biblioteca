using ProjectLibrary.Entities;
using ProjectLibrary.Entities.Enum;

namespace ProjectLibrary.Manager
{
    internal class ManageBook : Management
    {
        private string _pathBook = @"C:\Users\vitor\source\ProjectLibrary\Files\BookStorage.txt"; //path do File BookStorage
        private Dictionary<int, Book> ListBooks = new Dictionary<int, Book>();
        
        public ManageBook()
        {
            ReadFile();
        }

        public void ReadFile()
        {
            using (StreamReader sr = File.OpenText(_pathBook))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(',');
                    int ID = int.Parse(line[0]);
                    string name = line[1];
                    Category category = Enum.Parse<Category>(line[2]);
                    int quantity = int.Parse(line[3]);
                    Book NewBook = new Book(ID, name, category, quantity);
                    ListBooks[ID] = NewBook;
                }
            }
        }
        public void Listing()
        {
            var ListOrganize = ListBooks.OrderBy(book => book.Value.Category);
            foreach (var book in ListOrganize)
            { 
                Console.WriteLine(book.Value);
            }
        }
        
        public void Available()
        {
            var BookAvailable = ListBooks.Where(book => book.Value.Quantity > 0);
            foreach (var book in BookAvailable)
            {
                Console.WriteLine(book.Value);
            }
        }

        public void LoanBook(int ID)
        {
            if (ListBooks.ContainsKey(ID))
            {
                ListBooks[ID].Quantity -= 1;
            }
            else { Console.WriteLine("[ID invalidation]"); }
        }
        public void ReturnBook(int ID)
        {
            if (ListBooks.ContainsKey(ID))
            {
                ListBooks[ID].Quantity += 1;
            }
            else { Console.WriteLine("[ID invalidation]"); }
        }

        public void CheckBook(int IDbook)
        {
            Book? book = ListBooks[IDbook];
            if(book is Book B)
            {
                Console.WriteLine(B);
            }
            else { Console.WriteLine("[Error] Book ID not found "); }
        }

        public void WriteFile() 
        {
            using (StreamWriter sw = new StreamWriter(_pathBook, false))
            {
                foreach (var book in ListBooks)
                {
                    sw.WriteLine($"{book.Key},{book.Value.Name},{book.Value.Category},{book.Value.Quantity}");
                }
            }
        }
    }
}
