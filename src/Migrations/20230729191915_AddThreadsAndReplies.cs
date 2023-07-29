using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace boards.Migrations
{
    /// <inheritdoc />
    public partial class AddThreadsAndReplies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Threads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BoardSlug = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Threads_Boards_BoardSlug",
                        column: x => x.BoardSlug,
                        principalTable: "Boards",
                        principalColumn: "Slug",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    boardSlug = table.Column<string>(type: "TEXT", nullable: false),
                    ThreadDbId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Replies_Boards_boardSlug",
                        column: x => x.boardSlug,
                        principalTable: "Boards",
                        principalColumn: "Slug",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Replies_Threads_ThreadDbId",
                        column: x => x.ThreadDbId,
                        principalTable: "Threads",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Replies_boardSlug",
                table: "Replies",
                column: "boardSlug");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ThreadDbId",
                table: "Replies",
                column: "ThreadDbId");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_BoardSlug",
                table: "Threads",
                column: "BoardSlug");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "Threads");
        }
    }
}
