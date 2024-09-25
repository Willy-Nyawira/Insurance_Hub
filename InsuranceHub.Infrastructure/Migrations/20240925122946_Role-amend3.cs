using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Roleamend3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("21914e00-a29c-485d-a8ab-89464776f6cf"), "User" },
                    { new Guid("494e79d1-aed9-47d0-84c9-589f575752d2"), "Supervisor" },
                    { new Guid("7808cdc3-e9de-42cf-8b9f-d4a7d01b58e6"), "Admin" },
                    { new Guid("f86c3207-679a-49bb-8a73-289e8536b51a"), "Customer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
