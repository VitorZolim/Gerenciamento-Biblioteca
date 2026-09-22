using Library.EFCore.Context;
using LibraryDomain.Repositories;

namespace Library.EFCore.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository? _userRepo;

        private IBookRepository? _bookRepo;

        private IUserBookRepository? _userbookRepo;

        public AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepo = _userRepo ?? new UserRepository(_context);
            }
        }

        public IBookRepository BookRepository
        {
            get
            {
                return _bookRepo = _bookRepo ?? new BookRepository(_context);
            }
        }

        public IUserBookRepository UserBookRepository
        {
            get
            {
                return _userbookRepo = _userbookRepo ?? new UserBookRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
