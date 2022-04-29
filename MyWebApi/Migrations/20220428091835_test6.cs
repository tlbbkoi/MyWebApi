using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebApi.Migrations
{
    public partial class test6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "854a3f53-c30e-4a56-82cb-f5f2035aa85c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89cce2ac-c7bb-4a1c-bcec-f89b5e8e029f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b6106044-e2ba-4001-8440-6a0b75910310", "d2744316-e34d-4e43-9634-a211934ea029", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "caba5f24-4053-4977-a264-6c25eeb0707b", "d8b70ee8-27df-4bf7-a1af-2fae49ed4c07", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6106044-e2ba-4001-8440-6a0b75910310");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "caba5f24-4053-4977-a264-6c25eeb0707b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "89cce2ac-c7bb-4a1c-bcec-f89b5e8e029f", "212f4a99-6082-424b-8e80-b6e0c404ca92", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "854a3f53-c30e-4a56-82cb-f5f2035aa85c", "870f14e2-e87b-40e5-a775-d78241082635", "Administrator", "ADMINISTRATOR" });
        }
    }
}
