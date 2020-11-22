using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_category_user_UserId",
                schema: "smartsaver",
                table: "category");

            migrationBuilder.DropForeignKey(
                name: "FK_goal_user_UserId",
                schema: "smartsaver",
                table: "goal");

            migrationBuilder.DropForeignKey(
                name: "FK_transaction_user_UserId",
                schema: "smartsaver",
                table: "transaction");

            migrationBuilder.AddForeignKey(
                name: "FK_category_user_UserId",
                schema: "smartsaver",
                table: "category",
                column: "UserId",
                principalSchema: "smartsaver",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_goal_user_UserId",
                schema: "smartsaver",
                table: "goal",
                column: "UserId",
                principalSchema: "smartsaver",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transaction_user_UserId",
                schema: "smartsaver",
                table: "transaction",
                column: "UserId",
                principalSchema: "smartsaver",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_category_user_UserId",
                schema: "smartsaver",
                table: "category");

            migrationBuilder.DropForeignKey(
                name: "FK_goal_user_UserId",
                schema: "smartsaver",
                table: "goal");

            migrationBuilder.DropForeignKey(
                name: "FK_transaction_user_UserId",
                schema: "smartsaver",
                table: "transaction");

            migrationBuilder.AddForeignKey(
                name: "FK_category_user_UserId",
                schema: "smartsaver",
                table: "category",
                column: "UserId",
                principalSchema: "smartsaver",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_goal_user_UserId",
                schema: "smartsaver",
                table: "goal",
                column: "UserId",
                principalSchema: "smartsaver",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_transaction_user_UserId",
                schema: "smartsaver",
                table: "transaction",
                column: "UserId",
                principalSchema: "smartsaver",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
