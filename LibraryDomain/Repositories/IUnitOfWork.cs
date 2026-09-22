namespace LibraryDomain.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IBookRepository BookRepository { get; }
        IUserBookRepository UserBookRepository { get; }
        void Commit();
        Task CommitAsync();
    }
}
