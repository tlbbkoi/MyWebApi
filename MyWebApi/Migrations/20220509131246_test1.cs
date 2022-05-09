using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebApi.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4fc8d451-efc3-4bce-a563-3b502eabf591");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8daeb736-dc06-4258-a6a1-003b88f70e52");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8b242cc8-dd9c-4f9a-b071-aaa5af49af5e", "a1e45c1d-5b1b-4944-8681-89a0e1428275", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ed3086c9-3443-4d25-9ed6-4b6fa1631a63", "9608524d-0cd9-4feb-8c3d-99b239b71430", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b242cc8-dd9c-4f9a-b071-aaa5af49af5e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed3086c9-3443-4d25-9ed6-4b6fa1631a63");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8daeb736-dc06-4258-a6a1-003b88f70e52", "0933fdf3-1e44-4600-a09e-0589a37dd3ad", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4fc8d451-efc3-4bce-a563-3b502eabf591", "86a94d38-727a-4671-98f7-b226f80577b9", "Administrator", "ADMINISTRATOR" });
        }
    }
}
