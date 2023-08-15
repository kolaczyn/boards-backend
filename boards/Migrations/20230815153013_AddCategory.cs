using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace boards.Migrations
{
    /// <inheritdoc />
    public partial class AddCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Boards",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDb", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boards_CategoryId",
                table: "Boards",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_CategoryDb_CategoryId",
                table: "Boards",
                column: "CategoryId",
                principalTable: "CategoryDb",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_CategoryDb_CategoryId",
                table: "Boards");

            migrationBuilder.DropTable(
                name: "CategoryDb");

            migrationBuilder.DropIndex(
                name: "IX_Boards_CategoryId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Boards");
        }
    }
}
