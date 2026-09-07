namespace Library.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public UserBook? UserBook { get; set; }
    }
}
