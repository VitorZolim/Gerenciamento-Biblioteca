using Library.Domain.Entities;
using Library.EFCore.Context;
using LibraryDomain.Repositories;

namespace Library.EFCore.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }
    }
}
