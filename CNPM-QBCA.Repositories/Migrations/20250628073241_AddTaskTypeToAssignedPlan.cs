using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNPM_QBCA.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskTypeToAssignedPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Subjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
