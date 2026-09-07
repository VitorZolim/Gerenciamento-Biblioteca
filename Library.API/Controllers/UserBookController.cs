using Library.Domain.Entities;
using Library.Domain.Entities.Enum;
using Library.EFCore.Context;
using LibraryDomain.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserBookController : Controller
    {
        private readonly AppDbContext _context;

        public UserBookController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBookDTO>>> GetUserBooks()
        {
            var userbook = await _context.UserBooks.AsNoTracking().Include(u => u.User).Include(b => b.Book).ToListAsync();

            var UBResults = userbook.Select(ub => new UserBookDTO
            {
                UserId = ub.UserId,
                UserName = ub.User.UserName,
                BookId = ub.BookId,
                BookTitle = ub.Book.BookTitle,
                DateOutBook = ub.DateOutBook,
                DueBook = ub.DueBook,
                ReturnedBook = ub.ReturnedBook,
                Status = ub.Status
            });

            return Ok(UBResults);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<UserBookDTO>>> GetUserBooksByStatus(LoanStatus status)
        {
            IQueryable<UserBook> query = _context.UserBooks.AsNoTracking();
            var today = DateTime.UtcNow.Date;

            query = status switch
            {
                LoanStatus.Returned => query.Where(ub => ub.ReturnedBook != null),

                LoanStatus.Late => query.Where(ub =>
                        ub.ReturnedBook == null && ub.DueBook < today),

                LoanStatus.DueToday => query.Where(ub => 
                        ub.ReturnedBook == null && ub.DueBook >= today && ub.DueBook < today.AddDays(1)),

                LoanStatus.OnTime => query.Where(ub => ub.ReturnedBook == null && ub.DueBook >= today.AddDays(1)),

                _ => query //retorna query da forma que está
            };

            var userBooks = await query.Include(ub => ub.User).Include(ub => ub.Book).ToListAsync();
            var UBResults = userBooks.Select(ub => new UserBookDTO
            {
                UserId = ub.UserId,
                UserName = ub.User!.UserName,
                BookId = ub.BookId,
                BookTitle = ub.Book!.BookTitle,
                DateOutBook = ub.DateOutBook,
                DueBook = ub.DueBook,
                ReturnedBook = ub.ReturnedBook,
                Status = ub.Status
            });

            return Ok(UBResults);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserBook(CreateUserBookDTO dto)
        {
            //Validação de Entrada
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == dto.UserId);
            if (user is null)
                return NotFound($"User Id: {dto.UserId} not found.");

            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == dto.BookId);
            if (book is null)
                return NotFound($"Book Id: {dto.BookId} not found.");

            //Verifica se o usuario já possui um livro
            var userHasBook = await _context.UserBooks.AnyAsync(ub => ub.UserId == dto.UserId);
            if (userHasBook)
                return BadRequest("User already has a book.");

            var userBook = new UserBook
            {
                UserId = dto.UserId,
                BookId = dto.BookId
            };

            await _context.UserBooks.AddAsync(userBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserBooks), null, new UserBookDTO
            {
                    UserId = userBook.UserId,
                    UserName = user.UserName,
                    BookId = userBook.BookId,
                    BookTitle = book.BookTitle,
                    DateOutBook = userBook.DateOutBook,
                    DueBook = userBook.DueBook,
                    ReturnedBook = userBook.ReturnedBook,
                    Status = userBook.Status
            });
        }


        [HttpPut("{idUser:int},{idBook:int}")]
        public async Task<ActionResult> UpdateUser(int idUser, int idBook, UserBook userbook)
        {
            if (idUser != userbook.UserId || idBook != userbook.BookId)
                return BadRequest("UserId or BookId does not match");

            var verifyUserBook = await _context.UserBooks.FirstOrDefaultAsync(ub =>ub.UserId == idUser &&ub.BookId == idBook);

            if (verifyUserBook is null)
                return NotFound("UserBook not found.");

            verifyUserBook.ReturnedBook = userbook.ReturnedBook;

            await _context.SaveChangesAsync();

            return Ok(verifyUserBook);
        }

        [HttpDelete("{idUser:int},{idBook:int}")]
        public async Task<ActionResult<UserBook>> DeleteUserBook(int idUser, int idBook)
        {
            var userbook = await _context.UserBooks.FirstOrDefaultAsync(u => u.UserId == idUser && u.BookId == idBook);

            if (userbook is null)
                return NotFound($"Erro Not found");

            _context.UserBooks.Remove(userbook);
            await _context.SaveChangesAsync();

            return Ok(userbook);
        }
    }
}
