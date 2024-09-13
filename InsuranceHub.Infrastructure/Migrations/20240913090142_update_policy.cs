using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_policy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("40387dfb-0e44-4e72-9d4c-e5bf500316dc"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("444b1f2e-c642-45a6-9384-8ad7df7daf92"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e3791673-b629-48ac-ae3c-38f5c9e4068f"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Policies");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Policies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PolicyType",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PremiumAmount",
                table: "Policies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Policies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    PhysicalAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType" },
                values: new object[,]
                {
                    { new Guid("3f3d4cfa-6384-4252-a7f8-0a07f5046cde"), 2 },
                    { new Guid("47323ea7-00dd-4e98-9842-a74b8089d26d"), 1 },
                    { new Guid("7012fa65-067e-4989-865c-8f1e4f3af222"), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Policies_CustomerId",
                table: "Policies",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Customer_CustomerId",
                table: "Policies",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Customer_CustomerId",
                table: "Policies");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Policies_CustomerId",
                table: "Policies");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3f3d4cfa-6384-4252-a7f8-0a07f5046cde"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("47323ea7-00dd-4e98-9842-a74b8089d26d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7012fa65-067e-4989-865c-8f1e4f3af222"));

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "PolicyType",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "PremiumAmount",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Policies");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType" },
                values: new object[,]
                {
                    { new Guid("40387dfb-0e44-4e72-9d4c-e5bf500316dc"), 2 },
                    { new Guid("444b1f2e-c642-45a6-9384-8ad7df7daf92"), 0 },
                    { new Guid("e3791673-b629-48ac-ae3c-38f5c9e4068f"), 1 }
                });
        }
    }
}
