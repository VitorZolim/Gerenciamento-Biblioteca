using Library.Domain.Entities;
using Library.EFCore.Context;
using Library.EFCore.Repositories;
using LibraryDomain.Entities.DTOs;
using LibraryDomain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetUsers()
        {
            var users = await _unitofwork.UserRepository.GetUsersWithDetailsAsync();

            var usersResult = users.Select(u => new
            {
                u.UserId,
                u.UserName,
                UserBook = u.UserBook is null ? null : new UserDTO
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
        public async Task<ActionResult<object>> GetUser(int id)
        {
            var user = await _unitofwork.UserRepository.GetUserWithDetailsByIdAsync(id);

            if (user is null)
                return NotFound($"User Id: {id} not found");

            var result = new
            {
                user.UserId,
                user.UserName,
                UserBook = user.UserBook is null ? null : new UserDTO
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

            await _unitofwork.UserRepository.AddAsync(user);
            await _unitofwork.CommitAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateUser(int id, User user)
        {
            if (id != user.UserId)
                return BadRequest("Error updating User");

            _unitofwork.UserRepository.Update(user);
            await _unitofwork.CommitAsync();

            return Ok(user);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _unitofwork.UserRepository.GetByIdAsync(id);

            if (user is null)
                return NotFound($"User Id: {id} not found");

            _unitofwork.UserRepository.Delete(user);
            await _unitofwork.CommitAsync();

            return Ok(user);
        }
    }
}
