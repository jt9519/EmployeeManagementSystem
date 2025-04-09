using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryKeysV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Operation_EmployeeId",
                table: "Operation",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_Employee_EmployeeId",
                table: "Operation",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operation_Employee_EmployeeId",
                table: "Operation");

            migrationBuilder.DropIndex(
                name: "IX_Operation_EmployeeId",
                table: "Operation");
        }
    }
}
