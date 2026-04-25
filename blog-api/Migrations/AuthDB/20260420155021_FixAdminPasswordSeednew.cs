using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_api.Migrations.AuthDB
{
    /// <inheritdoc />
    public partial class FixAdminPasswordSeednew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d30574ac-2a0a-4f28-ab3f-822707a926e9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "90244ef5-d15a-4b14-8588-cb81a5afadfd", "AQAAAAIAAYagAAAAEGdPmrBkQhTSWyTjeM9W3Js8O2sOW/Qzdce7RY7nGIDpKB+TfTbATeB55bQVYwPLCA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d30574ac-2a0a-4f28-ab3f-822707a926e9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "97bcd92d-1816-4ebe-a383-79d828b70e93", "AQAAAAIAAYagAAAAEPkNFoLGULH3dpBdC28Bv1YIne8SAOb2CZKBFvHQUTzUETdeOZXlxdM318PmF5UJ0A==" });
        }
    }
}
