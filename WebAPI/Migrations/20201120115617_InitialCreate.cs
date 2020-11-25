using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WebAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "smartsaver");

            migrationBuilder.CreateTable(
                name: "user",
                schema: "smartsaver",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "category",
                schema: "smartsaver",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DedicatedAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_category_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "smartsaver",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "goal",
                schema: "smartsaver",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Deadlinedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Creationdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_goal_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "smartsaver",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                schema: "smartsaver",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Details = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CounterParty = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CategoryId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transaction_category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "smartsaver",
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_transaction_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "smartsaver",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_category_Title",
                schema: "smartsaver",
                table: "category",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_category_UserId",
                schema: "smartsaver",
                table: "category",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_goal_UserId",
                schema: "smartsaver",
                table: "goal",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_CategoryId",
                schema: "smartsaver",
                table: "transaction",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_UserId",
                schema: "smartsaver",
                table: "transaction",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "goal",
                schema: "smartsaver");

            migrationBuilder.DropTable(
                name: "transaction",
                schema: "smartsaver");

            migrationBuilder.DropTable(
                name: "category",
                schema: "smartsaver");

            migrationBuilder.DropTable(
                name: "user",
                schema: "smartsaver");
        }
    }
}
