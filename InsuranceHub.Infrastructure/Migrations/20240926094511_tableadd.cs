using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tableadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Customers_CustomerId",
                table: "Policies");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("951511b1-df81-418a-aed4-3db67b30d978"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a81e7c19-b6f8-44ce-8cd0-05f4c9e35472"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ad086ec8-eb06-4501-ae10-f2c90b66c191"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f0514691-20a3-4ff2-8627-f89928272b74"));

            migrationBuilder.AlterColumn<int>(
                name: "PolicyType",
                table: "Policies",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Policies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "PolicyType",
                table: "Invoices",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "PolicyCustomerAssociations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyCustomerAssociations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyCustomerAssociations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolicyCustomerAssociations_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType" },
                values: new object[,]
                {
                    { new Guid("46243ce9-0bc1-4b43-b56a-cae58ff6a2ad"), "Supervisor" },
                    { new Guid("48577008-c11b-4664-bc22-ba0be81fb36c"), "User" },
                    { new Guid("c137fb22-4f8f-4400-8f75-6f7b9eb8e650"), "Admin" },
                    { new Guid("dfbb2589-91c7-4ef7-a72d-b547c4d500f3"), "Customer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PolicyCustomerAssociations_CustomerId",
                table: "PolicyCustomerAssociations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyCustomerAssociations_PolicyId",
                table: "PolicyCustomerAssociations",
                column: "PolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Customers_CustomerId",
                table: "Policies",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Customers_CustomerId",
                table: "Policies");

            migrationBuilder.DropTable(
                name: "PolicyCustomerAssociations");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("46243ce9-0bc1-4b43-b56a-cae58ff6a2ad"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("48577008-c11b-4664-bc22-ba0be81fb36c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c137fb22-4f8f-4400-8f75-6f7b9eb8e650"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("dfbb2589-91c7-4ef7-a72d-b547c4d500f3"));

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Policies");

            migrationBuilder.AlterColumn<string>(
                name: "PolicyType",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Policies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PolicyType",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleType" },
                values: new object[,]
                {
                    { new Guid("951511b1-df81-418a-aed4-3db67b30d978"), "Supervisor" },
                    { new Guid("a81e7c19-b6f8-44ce-8cd0-05f4c9e35472"), "User" },
                    { new Guid("ad086ec8-eb06-4501-ae10-f2c90b66c191"), "Admin" },
                    { new Guid("f0514691-20a3-4ff2-8627-f89928272b74"), "Customer" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Customers_CustomerId",
                table: "Policies",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
