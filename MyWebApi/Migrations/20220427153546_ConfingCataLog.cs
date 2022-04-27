using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebApi.Migrations
{
    public partial class ConfingCataLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CataLogs_CataLogId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CataLogId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09bc68ab-7764-4f54-9444-05ac90283f98");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24ec5c3b-97a6-4f0b-b490-d1186a6870f9");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CataLogId",
                table: "CataLogs",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a0259a67-c2e3-441e-957e-3ae0c3a739ea", "9a7ec6a3-9e72-491e-91d2-11a751200ca0", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4f56536b-b88b-4e78-a42d-93f82927410f", "2f0970c2-9d61-4108-8568-d5edb4f3b4cd", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                table: "Products",
                column: "ProductId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Products_ProductId",
                table: "Products",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CataLogs_CataLogs_CataLogId",
                table: "CataLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Products_ProductId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_CataLogs_CataLogId",
                table: "CataLogs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f56536b-b88b-4e78-a42d-93f82927410f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0259a67-c2e3-441e-957e-3ae0c3a739ea");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CataLogId",
                table: "CataLogs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "09bc68ab-7764-4f54-9444-05ac90283f98", "f20a98f1-1e75-4471-9219-b39909fbfe23", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "24ec5c3b-97a6-4f0b-b490-d1186a6870f9", "da1d44e4-d85d-4cdf-8163-39f0bd4406d7", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CataLogId",
                table: "Products",
                column: "CataLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CataLogs_CataLogId",
                table: "Products",
                column: "CataLogId",
                principalTable: "CataLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
