using Microsoft.EntityFrameworkCore.Migrations;

namespace LabII.Migrations
{
    public partial class AddCommentsForExpenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ExpenseId",
                table: "Comments",
                column: "ExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Expenses_ExpenseId",
                table: "Comments",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Expenses_ExpenseId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ExpenseId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "Comments");
        }
    }
}
