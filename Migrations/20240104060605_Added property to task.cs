using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_list_backend.Migrations
{
    /// <inheritdoc />
    public partial class Addedpropertytotask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "List",
                table: "TodoTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "List",
                table: "TodoTasks");
        }
    }
}
