using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Roleamend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a648f63c-8c24-4a1f-bff1-f8a360309f48"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ce00e840-b958-480d-88c3-b6d13d3bdea0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("fe841a48-ea53-4eea-abdf-32c69ed92716"));

            migrationBuilder.AlterColumn<string>(
                name: "RoleType",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "RoleType",
                table: "Roles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType" },
                values: new object[,]
                {
                    { new Guid("a648f63c-8c24-4a1f-bff1-f8a360309f48"), 2 },
                    { new Guid("ce00e840-b958-480d-88c3-b6d13d3bdea0"), 1 },
                    { new Guid("fe841a48-ea53-4eea-abdf-32c69ed92716"), 0 }
                });
        }
    }
}
