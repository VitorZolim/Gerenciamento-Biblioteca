using ProjectLibrary.Entities;
using System.Globalization;

namespace ProjectLibrary.Manager
{
    internal class ManageUser : Management
    {
        private string _pathUser = @"C:\Users\vitor\source\ProjectLibrary\Files\UserStorage.txt"; //path do File UserStorage
        private Dictionary<string, User> ListUser = new Dictionary<string, User>();

        public ManageUser()
        {
            ReadFile();
        }

        public string? CreateUser(string id, string name, int IDbook)
        {
            if (id.StartsWith("T"))
            {
                ListUser[id] = new Teacher(id, name, IDbook);
                return "Create";
            }
            else if(id.StartsWith("S")) 
            {
                ListUser[id] = new Student(id, name, IDbook);
                return "Create";
            }
            else { return null; }
        }

        public int? RemoveUser(string id)
        {
            if (ListUser.ContainsKey(id))
            {
                ListUser.Remove(id);
                return ListUser[id].IDBook;
            }
            else { return null; }
        }

        public void ReadFile()
        {
            using (StreamReader sr = File.OpenText(_pathUser))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(',');
                    string ID = line[0];
                    string name = line[1];
                    int IDBook = int.Parse(line[2]);
                    DateTime date = DateTime.Parse(line[3]);
                    User NewUser = Identifier(ID, name, IDBook,date);
                    ListUser[ID] = NewUser; // Vai ter que identificar se é professor ou aluno e colocar os valores de data
                }
            }
        }
        public void Listing()
        {
            var ListOrganize = ListUser.OrderByDescending(c => c.Value.ID.StartsWith("T"));
            foreach (var user in ListOrganize)
            {
                Console.WriteLine(user.Value);
            }
        }
        public void WriteFile()
        {
            using (StreamWriter sw = new StreamWriter(_pathUser, false))
            {
                foreach (var user in ListUser)
                {
                    sw.WriteLine($"{user.Key},{user.Value.Name},{user.Value.IDBook},{user.Value.DateOutBook.ToString("d")}");
                }
            }
        }

        public User Identifier(string id, string name, int IDbook, DateTime date)
        {
            if (id.StartsWith("T"))
            {
                return new Teacher(id,name,IDbook, date);
            }
            else { return new Student(id, name, IDbook, date); }
        }
    }
}
