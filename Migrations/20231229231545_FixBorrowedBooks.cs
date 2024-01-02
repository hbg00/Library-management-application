using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class FixBorrowedBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Books_BookId",
                table: "BorrowedBooks");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedBooks_BookId",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "BorrowedBooks");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_IdBook",
                table: "BorrowedBooks",
                column: "IdBook");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Books_IdBook",
                table: "BorrowedBooks",
                column: "IdBook",
                principalTable: "Books",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Books_IdBook",
                table: "BorrowedBooks");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedBooks_IdBook",
                table: "BorrowedBooks");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "BorrowedBooks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_BookId",
                table: "BorrowedBooks",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Books_BookId",
                table: "BorrowedBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
