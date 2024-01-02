using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class FixBorrowedBooksv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BorrowedBooks_IdBorrowedBook",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdBorrowedBook",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdBorrowedBook",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BorrowedBooks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_UserId",
                table: "BorrowedBooks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_AspNetUsers_UserId",
                table: "BorrowedBooks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_AspNetUsers_UserId",
                table: "BorrowedBooks");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedBooks_UserId",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BorrowedBooks");

            migrationBuilder.AddColumn<int>(
                name: "IdBorrowedBook",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdBorrowedBook",
                table: "AspNetUsers",
                column: "IdBorrowedBook");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BorrowedBooks_IdBorrowedBook",
                table: "AspNetUsers",
                column: "IdBorrowedBook",
                principalTable: "BorrowedBooks",
                principalColumn: "Id");
        }
    }
}
