using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Roleamend2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6f989642-3006-4f12-9ab6-3b9d3f61f096"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bd43d907-4f7a-4891-9c05-badb696cc3ba"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("be360689-8447-4095-a97c-d0acdd318092"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType" },
                values: new object[,]
                {
                    { new Guid("1a81df43-6386-40b4-94e1-bb9c9fec5680"), "Customer" },
                    { new Guid("5402477f-3fb1-4e2c-9fad-32832c76db98"), "Supervisor" },
                    { new Guid("d97b7a2c-2067-46de-ab7e-9f3d83d7a00c"), "Admin" },
                    { new Guid("f45cc818-7107-4df8-b6e9-1afb7b5b2de2"), "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1a81df43-6386-40b4-94e1-bb9c9fec5680"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5402477f-3fb1-4e2c-9fad-32832c76db98"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d97b7a2c-2067-46de-ab7e-9f3d83d7a00c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f45cc818-7107-4df8-b6e9-1afb7b5b2de2"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType" },
                values: new object[,]
                {
                    { new Guid("6f989642-3006-4f12-9ab6-3b9d3f61f096"), "Admin" },
                    { new Guid("bd43d907-4f7a-4891-9c05-badb696cc3ba"), "Supervisor" },
                    { new Guid("be360689-8447-4095-a97c-d0acdd318092"), "User" }
                });
        }
    }
}
