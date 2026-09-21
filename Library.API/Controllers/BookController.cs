using Library.Domain.Entities;
using Library.EFCore.Context;
using LibraryDomain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book is null)
                return NotFound($"Book Id: {id} not found");

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook(Book book)
        {
            if (book is null)
                return BadRequest("Error Posting Book");

            await _bookRepository.AddAsync(book);

            return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateBook(int id, Book book)
        {
            if (id != book.BookId)
                return BadRequest("Error update Book");

            await _bookRepository.UpdateAsync(book);

            return Ok(book);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book is null)
                return NotFound($"Book Id: {id} not found");

            await _bookRepository.DeleteAsync(book);

            return Ok(book);
        }
    }
}
