using Library.Domain.Entities;
using Library.Domain.Entities.Enum;

namespace LibraryDomain.Repositories
{
    public interface IUserBookRepository : IRepository<UserBook>
    {
        Task<IEnumerable<UserBook>> GetUserBooksWithDetailsAsync();

        Task<IEnumerable<UserBook>> GetUserBooksByStatusAsync(LoanStatus status);

        Task<bool> UserHasBookAsync(int userId);

        Task<UserBook?> GetUserBookByIdsAsync(int userId, int bookId);
    }
}
