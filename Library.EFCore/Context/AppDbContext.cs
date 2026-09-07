using Microsoft.EntityFrameworkCore;
using Library.Domain.Entities;

namespace Library.EFCore.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppConfig.GetConnectionString());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBook>(builder =>
            {
                builder.HasKey(ub => new { ub.UserId, ub.BookId });

                builder.Property(ub => ub.DateOutBook)
                    .IsRequired()
                    .HasColumnType("datetime2");

                builder.Property(ub => ub.DueBook)
                       .IsRequired()
                       .HasColumnType("datetime2");

                builder.Property(ub => ub.ReturnedBook)
                    .IsRequired(false)
                    .HasColumnType("datetime2");

                builder.Ignore(x => x.Status);

                builder.HasOne(x => x.User)
                       .WithOne(x => x.UserBook)
                       .HasForeignKey<UserBook>(x => x.UserId)
                       .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(x => x.Book)
                       .WithMany(x => x.UserBooks)
                       .HasForeignKey(x => x.BookId)
                       .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Book>(builder =>
            {
                builder.HasKey(b => b.BookId);

                builder.Property(b => b.BookTitle)
                    .IsRequired()
                    .HasMaxLength(150);

                builder.Property(b => b.Quantity)
                    .IsRequired()
                    .HasDefaultValue(0);

                builder.Property(a => a.Author)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(u => u.UserId);

                builder.Property(u => u.UserName)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
