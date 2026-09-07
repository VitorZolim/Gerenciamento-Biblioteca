using Library.Domain.Entities;
using Library.EFCore.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly AppDbContext _context;
        public BookController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _context.Books.AsNoTracking().ToListAsync();

            return books;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.AsNoTracking().SingleOrDefaultAsync(d => d.BookId == id);
            if (book is null)
                return NotFound($"Book Id: {id} not found");

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook(Book book)
        {
            if (book is null)
                return BadRequest("Error Posting Book");

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetBook),
                new { id = book.BookId }, book);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateBook(int id, Book book)
        {
            if (id != book.BookId)
                return BadRequest("Error update Book");

            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
            if (book is null)
                return NotFound($"Book Id: {id} not found");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(book);
        }
    }
}
