using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace boards.Migrations
{
    /// <inheritdoc />
    public partial class FixRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Boards_boardSlug",
                table: "Replies");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Threads_ThreadDbId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_boardSlug",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_ThreadDbId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "ThreadDbId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "boardSlug",
                table: "Replies");

            migrationBuilder.AddColumn<int>(
                name: "ThreadId",
                table: "Replies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ThreadId",
                table: "Replies",
                column: "ThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Threads_ThreadId",
                table: "Replies",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Threads_ThreadId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_ThreadId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "ThreadId",
                table: "Replies");

            migrationBuilder.AddColumn<int>(
                name: "ThreadDbId",
                table: "Replies",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "boardSlug",
                table: "Replies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_boardSlug",
                table: "Replies",
                column: "boardSlug");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ThreadDbId",
                table: "Replies",
                column: "ThreadDbId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Boards_boardSlug",
                table: "Replies",
                column: "boardSlug",
                principalTable: "Boards",
                principalColumn: "Slug",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Threads_ThreadDbId",
                table: "Replies",
                column: "ThreadDbId",
                principalTable: "Threads",
                principalColumn: "Id");
        }
    }
}
