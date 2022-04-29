using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebApi.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28836e29-b129-437a-8620-15d62fdaad2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a617c614-1447-4469-9499-9492a0ead9f2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9cb73954-509b-4ee6-a225-76cc07d2d2f2", "e9de477d-720e-45e2-a8f5-18a83b811e6a", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c2f080af-f206-43d9-803e-7afd26fa8369", "f55fd3f4-265a-4b3f-bc83-b939e836b76f", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cb73954-509b-4ee6-a225-76cc07d2d2f2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2f080af-f206-43d9-803e-7afd26fa8369");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a617c614-1447-4469-9499-9492a0ead9f2", "50dc101f-2bfb-4af2-b30f-c93d3e2bc064", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "28836e29-b129-437a-8620-15d62fdaad2c", "6356df75-65d7-4b40-b175-b9c1f1135b10", "Administrator", "ADMINISTRATOR" });
        }
    }
}
