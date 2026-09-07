using Library.Domain.Entities;
using Library.EFCore.Context;
using LibraryDomain.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.AsNoTracking().Include(u => u.UserBook).ThenInclude(ub => ub.Book).ToListAsync();

            var usersResult = users.Select(u => new
            {
                u.UserId,
                u.UserName,
                UserBook = u.UserBook is null ? null : new UserBookDto 
                {
                    BookId = u.UserBook.BookId,
                    BookTitle = u.UserBook.Book!.BookTitle,
                    DateOutBook = u.UserBook.DateOutBook,
                    DueBook = u.UserBook.DueBook,
                    ReturnedBook = u.UserBook.ReturnedBook,
                    Status = u.UserBook.Status
                }
            });

            return Ok(usersResult);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.AsNoTracking().Include(u => u.UserBook).ThenInclude(ub => ub.Book).SingleOrDefaultAsync(u => u.UserId == id);
            if (user is null)
                return NotFound($"User Id: {id} not found");

            var result = new
            {
                user.UserId,
                user.UserName,
                UserBook = user.UserBook is null ? null : new UserBookDto 
                {
                    BookId = user.UserBook.BookId,
                    BookTitle = user.UserBook.Book!.BookTitle,
                    DateOutBook = user.UserBook.DateOutBook,
                    DueBook = user.UserBook.DueBook,
                    ReturnedBook = user.UserBook.ReturnedBook,
                    Status = user.UserBook.Status
                }
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            if (user is null)
                return BadRequest("Error Posting User");

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser),
                new { id = user.UserId }, user);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateUser(int id, User user)
        {
            if (id != user.UserId)
                return BadRequest("Error updating User");

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if (user is null)
                return NotFound($"User Id: {id} not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }
    }

}
