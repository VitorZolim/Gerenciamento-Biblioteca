using Library.Domain.Entities;
using Library.EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.EFCore.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetUsersWithDetailsAsync()
        {
            return await _context.Users.AsNoTracking()
                .Include(u => u.UserBook).ThenInclude(ub => ub.Book)
                .ToListAsync();
        }

        public async Task<User?> GetUserWithDetailsByIdAsync(int id)
        {
            return await _context.Users.AsNoTracking()
                .Include(u => u.UserBook).ThenInclude(ub => ub.Book)
                .SingleOrDefaultAsync(u => u.UserId == id);
        }
    }
}
