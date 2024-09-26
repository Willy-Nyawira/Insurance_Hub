using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("21914e00-a29c-485d-a8ab-89464776f6cf"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("494e79d1-aed9-47d0-84c9-589f575752d2"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7808cdc3-e9de-42cf-8b9f-d4a7d01b58e6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f86c3207-679a-49bb-8a73-289e8536b51a"));

            migrationBuilder.AddColumn<int>(
                name: "PaymentFrequency",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PolicyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentFrequency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType" },
                values: new object[,]
                {
                    { new Guid("08a62c67-e921-4201-918c-bc70ee6b145a"), "Supervisor" },
                    { new Guid("aff3cb80-1b5b-458c-9e9b-8be9121a4cbf"), "Customer" },
                    { new Guid("d07f2d21-18ed-4012-9541-aae9d91d803c"), "Admin" },
                    { new Guid("ff2f1abb-8f60-4f69-8e4f-a5684a047b0e"), "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("08a62c67-e921-4201-918c-bc70ee6b145a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aff3cb80-1b5b-458c-9e9b-8be9121a4cbf"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d07f2d21-18ed-4012-9541-aae9d91d803c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ff2f1abb-8f60-4f69-8e4f-a5684a047b0e"));

            migrationBuilder.DropColumn(
                name: "PaymentFrequency",
                table: "Policies");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType" },
                values: new object[,]
                {
                    { new Guid("21914e00-a29c-485d-a8ab-89464776f6cf"), "User" },
                    { new Guid("494e79d1-aed9-47d0-84c9-589f575752d2"), "Supervisor" },
                    { new Guid("7808cdc3-e9de-42cf-8b9f-d4a7d01b58e6"), "Admin" },
                    { new Guid("f86c3207-679a-49bb-8a73-289e8536b51a"), "Customer" }
                });
        }
    }
}
