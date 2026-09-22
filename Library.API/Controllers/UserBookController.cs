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
        // Necessario os 3 repositórios para as validações
        private readonly IUnitOfWork _unitofwork;
        public UserBookController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBookDTO>>> GetUserBooks()
        {
            var userbooks = await _unitofwork.UserBookRepository.GetUserBooksWithDetailsAsync();

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
            var userBooks = await _unitofwork.UserBookRepository.GetUserBooksByStatusAsync(status);

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
            var user = await _unitofwork.UserRepository.GetByIdAsync(dto.UserId);
            if (user is null)
                return NotFound($"User Id: {dto.UserId} not found.");

            //2. Valida se o livro existe usando o repositório de livros
            var book = await _unitofwork.BookRepository.GetByIdAsync(dto.BookId);
            if (book is null)
                return NotFound($"Book Id: {dto.BookId} not found.");

            //3. Verifica se o usuario já possui um livro usando o método específico
            var userHasBook = await _unitofwork.UserBookRepository.UserHasBookAsync(dto.UserId);
            if (userHasBook)
                return BadRequest("User already has a book.");

            var userBook = new UserBook
            {
                UserId = dto.UserId,
                BookId = dto.BookId
            };

            await _unitofwork.UserBookRepository.AddAsync(userBook);
            await _unitofwork.CommitAsync();

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

            var verifyUserBook = await _unitofwork.UserBookRepository.GetByIdAsync(idUser, idBook);

            if (verifyUserBook is null)
                return NotFound("UserBook not found.");

            verifyUserBook.ReturnedBook = userbook.ReturnedBook;

            _unitofwork.UserBookRepository.Update(verifyUserBook);
            await _unitofwork.CommitAsync();

            return Ok(verifyUserBook);
        }

        [HttpDelete("{idUser:int},{idBook:int}")]
        public async Task<ActionResult<UserBook>> DeleteUserBook(int idUser, int idBook)
        {
            var userbook = await _unitofwork.UserBookRepository.GetByIdAsync(idUser, idBook);

            if (userbook is null)
                return NotFound($"Erro Not found");

            _unitofwork.UserBookRepository.Delete(userbook);
            await _unitofwork.CommitAsync();

            return Ok(userbook);
        }
    }
}
