using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduVersity.Migrations
{
    /// <inheritdoc />
    public partial class AddSemesterOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SemesterOrder",
                table: "Semesters",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SemesterOrder",
                table: "Semesters");
        }
    }
}
