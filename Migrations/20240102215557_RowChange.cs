using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class RowChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_AspNetUsers_UserId",
                table: "BorrowedBooks");

            migrationBuilder.RenameColumn(
                name: "DateOfBrith",
                table: "Publishers",
                newName: "DateOfBirth");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BorrowedBooks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Publishers",
                newName: "DateOfBrith");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BorrowedBooks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_AspNetUsers_UserId",
                table: "BorrowedBooks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
