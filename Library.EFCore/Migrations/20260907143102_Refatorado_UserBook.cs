using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Refatorado_UserBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReturnBook",
                table: "UserBooks",
                newName: "DueBook");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnedBook",
                table: "UserBooks",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnedBook",
                table: "UserBooks");

            migrationBuilder.RenameColumn(
                name: "DueBook",
                table: "UserBooks",
                newName: "ReturnBook");
        }
    }
}
