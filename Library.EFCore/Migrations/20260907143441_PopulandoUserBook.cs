using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class PopulandoUserBook : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
        // OnTime
        mb.Sql("""
        INSERT INTO UserBooks (UserId, BookId, DateOutBook, DueBook, ReturnedBook)
        VALUES (1, 1, '2026-09-05', '2026-09-12', NULL)
        """);

        mb.Sql("""
        INSERT INTO UserBooks (UserId, BookId, DateOutBook, DueBook, ReturnedBook)
        VALUES (2, 6, '2026-09-04', '2026-09-11', NULL)
        """);

        mb.Sql("""
        INSERT INTO UserBooks (UserId, BookId, DateOutBook, DueBook, ReturnedBook)
        VALUES (3, 11, '2026-09-03', '2026-09-10', NULL)
        """);

        mb.Sql("""
        INSERT INTO UserBooks (UserId, BookId, DateOutBook, DueBook, ReturnedBook)
        VALUES (4, 15, '2026-09-02', '2026-09-09', NULL)
        """);

        // DueToday
        mb.Sql("""
        INSERT INTO UserBooks (UserId, BookId, DateOutBook, DueBook, ReturnedBook)
        VALUES (5, 20, '2026-08-31', '2026-09-07', NULL)
        """);

        mb.Sql("""
        INSERT INTO UserBooks (UserId, BookId, DateOutBook, DueBook, ReturnedBook)
        VALUES (6, 25, '2026-08-31', '2026-09-07', NULL)
        """);

        // Late
        mb.Sql("""
        INSERT INTO UserBooks (UserId, BookId, DateOutBook, DueBook, ReturnedBook)
        VALUES (7, 3, '2026-08-20', '2026-08-27', NULL)
        """);

        mb.Sql("""
        INSERT INTO UserBooks (UserId, BookId, DateOutBook, DueBook, ReturnedBook)
        VALUES (8, 8, '2026-08-25', '2026-09-01', NULL)
        """);

        mb.Sql("""
        INSERT INTO UserBooks (UserId, BookId, DateOutBook, DueBook, ReturnedBook)
        VALUES (9, 13, '2026-08-15', '2026-08-22', NULL)
        """);

        mb.Sql("""
        INSERT INTO UserBooks (UserId, BookId, DateOutBook, DueBook, ReturnedBook)
        VALUES (10, 18, '2026-08-28', '2026-09-04', NULL)
        """);

        // Returned
        mb.Sql("""
        INSERT INTO UserBooks (UserId, BookId, DateOutBook, DueBook, ReturnedBook)
        VALUES (11, 2, '2026-08-20', '2026-08-27', '2026-08-25')
        """);

        mb.Sql("""
        INSERT INTO UserBooks (UserId, BookId, DateOutBook, DueBook, ReturnedBook)
        VALUES (12, 7, '2026-08-22', '2026-08-29', '2026-08-28')
        """);

        mb.Sql("""
        INSERT INTO UserBooks (UserId, BookId, DateOutBook, DueBook, ReturnedBook)
        VALUES (13, 12, '2026-08-25', '2026-09-01', '2026-08-30')
        """);

        mb.Sql("""
        INSERT INTO UserBooks (UserId, BookId, DateOutBook, DueBook, ReturnedBook)
        VALUES (14, 17, '2026-08-28', '2026-09-04', '2026-09-03')
        """);

        mb.Sql("""
        INSERT INTO UserBooks (UserId, BookId, DateOutBook, DueBook, ReturnedBook)
        VALUES (15, 22, '2026-09-01', '2026-09-08', '2026-09-05')
        """);
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM UserBooks");
        }

    }
}
