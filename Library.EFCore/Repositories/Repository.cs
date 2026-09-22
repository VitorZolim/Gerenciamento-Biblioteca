using Library.EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.EFCore.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(params object[] keyValues)
        {
            return await _context.Set<T>().FindAsync(keyValues);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            //await _context.SaveChangesAsync();
        }

        public void Update(T entity) //volta a ser sincrono, Update muda o estado da entidade, não vai direto ao banco
        {
            _context.Set<T>().Update(entity);
            //_context.Entry(entity).State = EntityState.Modified; Segunda Forma de mudar
            //await _context.SaveChangesAsync();
        }

        public void Delete(T entity) //mesma ideia do Update, apenas muda o estado sem ir ao banco
        {
            _context.Set<T>().Remove(entity);
            //await _context.SaveChangesAsync();
        }
    }
}
