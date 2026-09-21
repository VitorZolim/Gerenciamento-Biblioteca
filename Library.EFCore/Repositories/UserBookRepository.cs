using Library.Domain.Entities;
using Library.Domain.Entities.Enum;
using Library.EFCore.Context;
using LibraryDomain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.EFCore.Repositories
{
    public class UserBookRepository : Repository<UserBook>, IUserBookRepository
    {
        public UserBookRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UserBook>> GetUserBooksWithDetailsAsync()
        {
            return await _context.Set<UserBook>().AsNoTracking()
                .Include(u => u.User).Include(b => b.Book)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserBook>> GetUserBooksByStatusAsync(LoanStatus status)
        {
            IQueryable<UserBook> query = _context.Set<UserBook>().AsNoTracking();
            var today = DateTime.UtcNow.Date;

            query = status switch
            {
                LoanStatus.Returned => query.Where(ub => ub.ReturnedBook != null),

                LoanStatus.Late => query.Where(ub =>
                    ub.ReturnedBook == null && ub.DueBook < today),

                LoanStatus.DueToday => query.Where(ub =>
                    ub.ReturnedBook == null && ub.DueBook >= today && ub.DueBook < today.AddDays(1)),

                LoanStatus.OnTime => query.Where(ub =>
                    ub.ReturnedBook == null && ub.DueBook >= today.AddDays(1)),

                _ => query
            };

            return await query.Include(ub => ub.User).Include(ub => ub.Book)
                .ToListAsync();
        }

        public async Task<bool> UserHasBookAsync(int userId)
        {
            return await _context.Set<UserBook>().AnyAsync(ub => ub.UserId == userId);
        }

        public async Task<UserBook?> GetUserBookByIdsAsync(int userId, int bookId)
        {
            return await _context.Set<UserBook>()
                .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BookId == bookId);
        }
    }
}
