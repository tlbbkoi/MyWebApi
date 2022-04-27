using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebApi.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Products_ProductId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductId",
                table: "Products");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "330d3367-0823-4ab3-bc9a-dafd064cb0c4", "f784b776-b15f-4793-80d7-6e402eb71f3c", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "821db1fb-8d9c-4fb2-bcee-53951b6e4d12", "294e82c8-b973-43ad-88f3-3c6aa79bde73", "Administrator", "ADMINISTRATOR" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValue: "330d3367-0823-4ab3-bc9a-dafd064cb0c4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "821db1fb-8d9c-4fb2-bcee-53951b6e4d12");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Products",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Products_ProductId",
                table: "Products",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
