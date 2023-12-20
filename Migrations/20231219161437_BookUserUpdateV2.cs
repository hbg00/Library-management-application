using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class BookUserUpdateV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Books_IdLanguage",
                table: "Books",
                column: "IdLanguage");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Languages_IdLanguage",
                table: "Books",
                column: "IdLanguage",
                principalTable: "Languages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Languages_IdLanguage",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_IdLanguage",
                table: "Books");
        }
    }
}
