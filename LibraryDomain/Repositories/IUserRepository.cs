using Library.Domain.Entities;

public interface IUserRepository : IRepository<User>
{
    //Métodos para trazer User junto de UserBook caso tenha um Book
    Task<IEnumerable<User>> GetUsersWithDetailsAsync();
    Task<User?> GetUserWithDetailsByIdAsync(int id);
}
