using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace boards.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryLol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_CategoryDb_CategoryId",
                table: "Boards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryDb",
                table: "CategoryDb");

            migrationBuilder.RenameTable(
                name: "CategoryDb",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Categories_CategoryId",
                table: "Boards",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Categories_CategoryId",
                table: "Boards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "CategoryDb");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryDb",
                table: "CategoryDb",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_CategoryDb_CategoryId",
                table: "Boards",
                column: "CategoryId",
                principalTable: "CategoryDb",
                principalColumn: "Id");
        }
    }
}
