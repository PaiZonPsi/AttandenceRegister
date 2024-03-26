using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeSummaryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeSummaries",
                columns: table => new
                {
                    Year = table.Column<int>(type: "int", nullable: false),
                    EmployeeFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccurrenceTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AbsentDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeSummaries");
        }
    }
}
