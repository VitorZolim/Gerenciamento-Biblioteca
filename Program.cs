using ProjectLibrary.Manager;
using ProjectLibrary.Entities;

namespace ProjectLibrary;

public class Program
{
    public static void Main()
    {
        ManageBook StartFileBook = new ManageBook();
        ManageUser StartFileUser = new ManageUser();
        Loan StartLoan = new Loan(StartFileBook, StartFileUser);

        Console.WriteLine("Library Management");
        bool repeat = true;
        while (repeat)
        {
            Console.WriteLine("1. List Books" +
                            "\n2. List Users" +
                            "\n3. Loan Book" +
                            "\n4. Return Book" +
                            "\n0. Exit");
            int option = int.Parse(Console.ReadLine());
            Console.Clear();

            switch (option)
            {
                case 1:
                    StartFileBook.Listing();
                    break;
                case 2:
                    StartFileUser.Listing();
                    Console.Write("Check User Book?(y/n) ");
                    char c = char.Parse(Console.ReadLine().ToLower());
                    if (c == 'y')
                    {
                        Console.Write("Book ID: ");
                        int IDbook = int.Parse(Console.ReadLine());
                        StartFileBook.CheckBook(IDbook);
                    }
                    break;
                case 3:
                    StartLoan.NewLoan();
                    break;
                case 4:
                    StartLoan.Return();
                    break;
                case 0:
                    repeat = false;
                    break;
                default:
                    Console.WriteLine("Invalid");
                    break;
            }
        }
        StartFileBook.WriteFile();
        StartFileUser.WriteFile();
    }
}