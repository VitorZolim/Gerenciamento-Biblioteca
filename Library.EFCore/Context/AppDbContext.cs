using Microsoft.EntityFrameworkCore;
using Library.Domain.Entities;

namespace Library.EFCore.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppConfig.GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorBook>().HasKey(fp => new { fp.AuthorId, fp.BookId });
            modelBuilder.Entity<UserBook>().HasKey(fp => new { fp.UserId, fp.BookId });

        }
    }
}
