using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class ExtendExsistingUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdBorrowedBook",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BorrowedBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfBorrowedCopies = table.Column<int>(type: "int", nullable: true),
                    CanBorrow = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBorrow = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdBook = table.Column<int>(type: "int", nullable: true),
                    BookId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowedBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdBorrowedBook",
                table: "AspNetUsers",
                column: "IdBorrowedBook");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_BookId",
                table: "BorrowedBooks",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BorrowedBooks_IdBorrowedBook",
                table: "AspNetUsers",
                column: "IdBorrowedBook",
                principalTable: "BorrowedBooks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BorrowedBooks_IdBorrowedBook",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BorrowedBooks");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdBorrowedBook",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdBorrowedBook",
                table: "AspNetUsers");
        }
    }
}
