using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace boards.Migrations
{
    /// <inheritdoc />
    public partial class AddTripcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tripcode",
                table: "Replies",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tripcode",
                table: "Replies");
        }
    }
}
