namespace ProjectLibrary.Manager
{
    internal class Loan
    {
        public ManageBook manageBook { get; set; }
        public ManageUser manageUser { get; set; }

        public Loan(ManageBook manageBook, ManageUser manageUser)
        {
            this.manageBook = manageBook;
            this.manageUser = manageUser;
        }

        public void NewLoan()
        {
            Console.Write("ID: ");
            string id = Console.ReadLine();
            Console.Write("Name: ");
            string name = Console.ReadLine();
            manageBook.Available();
            Console.WriteLine("ID Book: ");
            int Idbook = int.Parse(Console.ReadLine());
            
            manageUser.CreateUser(id, name, Idbook);
            manageBook.LoanBook(Idbook);
        }

        public void Return()
        {
            Console.Write("ID User: ");
            string id = Console.ReadLine();
            if (manageUser.RemoveUser(id) is int idbook)
            {
                manageBook.ReturnBook(idbook);
            }
            else { Console.WriteLine("Try Again"); }
        }
    }
}
