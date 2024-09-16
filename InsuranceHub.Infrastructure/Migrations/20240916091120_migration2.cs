using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Customers_CustomerId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Users_UserId",
                table: "Policies");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b96380f-fa2d-48a9-a087-a8bd12295e9f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b31f4610-7e18-463a-9ba3-b33b589e6a90"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c6c89ad8-b587-461c-8dd9-377a1b453398"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dob",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType" },
                values: new object[,]
                {
                    { new Guid("a648f63c-8c24-4a1f-bff1-f8a360309f48"), 2 },
                    { new Guid("ce00e840-b958-480d-88c3-b6d13d3bdea0"), 1 },
                    { new Guid("fe841a48-ea53-4eea-abdf-32c69ed92716"), 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Customers_CustomerId",
                table: "Policies",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Users_UserId",
                table: "Policies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Customers_CustomerId",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Users_UserId",
                table: "Policies");

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

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Policies");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Dob",
                table: "Customers",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType" },
                values: new object[,]
                {
                    { new Guid("9b96380f-fa2d-48a9-a087-a8bd12295e9f"), 1 },
                    { new Guid("b31f4610-7e18-463a-9ba3-b33b589e6a90"), 2 },
                    { new Guid("c6c89ad8-b587-461c-8dd9-377a1b453398"), 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Customers_CustomerId",
                table: "Policies",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Users_UserId",
                table: "Policies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
