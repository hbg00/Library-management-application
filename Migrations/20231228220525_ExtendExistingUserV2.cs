using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class ExtendExistingUserV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanBorrow",
                table: "BorrowedBooks");

            migrationBuilder.AddColumn<bool>(
                name: "CanBorrow",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanBorrow",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "CanBorrow",
                table: "BorrowedBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
