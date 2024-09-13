using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_policy2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Customer_CustomerId",
                table: "Policies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

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

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Customers_CustomerId",
                table: "Policies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

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

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType" },
                values: new object[,]
                {
                    { new Guid("3f3d4cfa-6384-4252-a7f8-0a07f5046cde"), 2 },
                    { new Guid("47323ea7-00dd-4e98-9842-a74b8089d26d"), 1 },
                    { new Guid("7012fa65-067e-4989-865c-8f1e4f3af222"), 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Customer_CustomerId",
                table: "Policies",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
