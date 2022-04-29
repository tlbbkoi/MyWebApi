using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebApi.Migrations
{
    public partial class test7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CataLogs_CataLogs_CataLogId",
                table: "CataLogs");

            migrationBuilder.DropIndex(
                name: "IX_CataLogs_CataLogId",
                table: "CataLogs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6106044-e2ba-4001-8440-6a0b75910310");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "caba5f24-4053-4977-a264-6c25eeb0707b");

            migrationBuilder.DropColumn(
                name: "CataLogId",
                table: "CataLogs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8daeb736-dc06-4258-a6a1-003b88f70e52", "0933fdf3-1e44-4600-a09e-0589a37dd3ad", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4fc8d451-efc3-4bce-a563-3b502eabf591", "86a94d38-727a-4671-98f7-b226f80577b9", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4fc8d451-efc3-4bce-a563-3b502eabf591");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8daeb736-dc06-4258-a6a1-003b88f70e52");

            migrationBuilder.AddColumn<int>(
                name: "CataLogId",
                table: "CataLogs",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b6106044-e2ba-4001-8440-6a0b75910310", "d2744316-e34d-4e43-9634-a211934ea029", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "caba5f24-4053-4977-a264-6c25eeb0707b", "d8b70ee8-27df-4bf7-a1af-2fae49ed4c07", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_CataLogs_CataLogId",
                table: "CataLogs",
                column: "CataLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_CataLogs_CataLogs_CataLogId",
                table: "CataLogs",
                column: "CataLogId",
                principalTable: "CataLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
