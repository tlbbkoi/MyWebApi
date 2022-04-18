using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebApi.Migrations
{
    public partial class SeeDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CataLogs",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Detergents" });

            migrationBuilder.InsertData(
                table: "CataLogs",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Tv" });

            migrationBuilder.InsertData(
                table: "CataLogs",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Mobie Phone" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CataLogId", "Context", "Discount", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Dung dịch tẩy rửa bồn cầu ", (byte)0, "Vim", 45000.0 },
                    { 2, 2, " Tx xem siêu nét từ Vinmart", (byte)10, "Tv Vinmart", 500000.0 },
                    { 3, 3, "Điện Thoại Sam Sung mới nhất", (byte)5, "SamSung Galaxy S10", 1200000.0 },
                    { 4, 3, "Điện Thoại Apple mới nhất", (byte)10, "Iphone 11", 1500000.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CataLogs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CataLogs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CataLogs",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
