using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
