using Library.Domain.Entities;
using Library.Domain.Entities.Enum;
using Library.EFCore.Context;
using LibraryDomain.Entities.DTOs;
using LibraryDomain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserBookController : Controller
    {
        // Necessario injetar os 3 repositórios para as validações
        private readonly IUserBookRepository _userBookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
 
        public UserBookController(IUserBookRepository userBookRepository,IUserRepository userRepository,IBookRepository bookRepository)
        {
            _userBookRepository = userBookRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBookDTO>>> GetUserBooks()
        {
            var userbooks = await _userBookRepository.GetUserBooksWithDetailsAsync();

            var UBResults = userbooks.Select(ub => new UserBookDTO
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
            var userBooks = await _userBookRepository.GetUserBooksByStatusAsync(status);

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
            //1. Valida se o usuário existe usando o repositório de usuarios
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user is null)
                return NotFound($"User Id: {dto.UserId} not found.");

            //2. Valida se o livro existe usando o repositório de livros
            var book = await _bookRepository.GetByIdAsync(dto.BookId);
            if (book is null)
                return NotFound($"Book Id: {dto.BookId} not found.");

            //3. Verifica se o usuario já possui um livro usando o método específico
            var userHasBook = await _userBookRepository.UserHasBookAsync(dto.UserId);
            if (userHasBook)
                return BadRequest("User already has a book.");

            var userBook = new UserBook
            {
                UserId = dto.UserId,
                BookId = dto.BookId
            };

            await _userBookRepository.AddAsync(userBook);

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
        public async Task<ActionResult> UpdateUserBook(int idUser, int idBook, UserBook userbook)
        {
            if (idUser != userbook.UserId || idBook != userbook.BookId)
                return BadRequest("UserId or BookId does not match");

            var verifyUserBook = await _userBookRepository.GetByIdAsync(idUser, idBook);

            if (verifyUserBook is null)
                return NotFound("UserBook not found.");

            verifyUserBook.ReturnedBook = userbook.ReturnedBook;

            await _userBookRepository.UpdateAsync(verifyUserBook);

            return Ok(verifyUserBook);
        }

        [HttpDelete("{idUser:int},{idBook:int}")]
        public async Task<ActionResult<UserBook>> DeleteUserBook(int idUser, int idBook)
        {
            var userbook = await _userBookRepository.GetByIdAsync(idUser, idBook);

            if (userbook is null)
                return NotFound($"Erro Not found");

            await _userBookRepository.DeleteAsync(userbook);

            return Ok(userbook);
        }
    }
}
