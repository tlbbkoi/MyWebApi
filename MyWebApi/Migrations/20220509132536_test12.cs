using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebApi.Migrations
{
    public partial class test12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "9cc7bd23-9fce-4687-84e7-94c531d1b2b4", "ef0c5acf-4377-45d9-8c54-a2c5845a2571", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "87377e25-a032-4796-96e2-33de30ec0c53", "e2211fbc-b90d-46ea-a3ac-aab97b660d1f", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87377e25-a032-4796-96e2-33de30ec0c53");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cc7bd23-9fce-4687-84e7-94c531d1b2b4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8b242cc8-dd9c-4f9a-b071-aaa5af49af5e", "a1e45c1d-5b1b-4944-8681-89a0e1428275", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ed3086c9-3443-4d25-9ed6-4b6fa1631a63", "9608524d-0cd9-4feb-8c3d-99b239b71430", "Administrator", "ADMINISTRATOR" });
        }
    }
}
