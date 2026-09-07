using Library.Domain.Entities.Enum;

namespace Library.Domain.Entities
{
    public class UserBook
    {
        private const int LoanDays = 7;
        public int UserId { get; set; }
        public User? User { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public DateTime DateOutBook { get; set; }
        public DateTime DueBook { get; set; }
        public DateTime? ReturnedBook { get; set; }

        public LoanStatus Status
        {
            get
            {
                if (ReturnedBook.HasValue)
                    return LoanStatus.Returned;

                var today = DateTime.UtcNow.Date;
                var returnDate = DueBook.Date;

                if (today > returnDate)
                    return LoanStatus.Late;

                if (today == returnDate)
                    return LoanStatus.DueToday;

                return LoanStatus.OnTime;
            }
        }

        public UserBook()
        {
            DateOutBook = DateTime.UtcNow;
            DueBook = DateOutBook.AddDays(LoanDays);
        }
    }
}
