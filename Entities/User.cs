namespace ProjectLibrary.Entities
{
    internal abstract class User
    {
        public string ID { get; set; } //Modelo do ID: P ou T se for professor de resto é aleatorio
        public string Name { get; set; }
        public int IDBook { get; set; } 
        public DateTime DateOutBook { get; set; }
        public User(string id, string name, int IDbook) 
        { 
            ID = id;
            Name = name;
            IDBook = IDbook;
            DateOutBook = DateTime.Now;
        }

        public abstract override string ToString();
    }
}
