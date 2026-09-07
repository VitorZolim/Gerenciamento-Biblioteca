using Library.Domain.Entities.Enum;

namespace LibraryDomain.Entities.DTOs
{
    public class UserDTO
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public DateTime DateOutBook { get; set; }
        public DateTime DueBook { get; set; }
        public DateTime? ReturnedBook { get; set; }
        public LoanStatus Status { get; set; }
    }
}
