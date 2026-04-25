using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_api.Migrations.AuthDB
{
    /// <inheritdoc />
    public partial class FixAdminPasswordSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d30574ac-2a0a-4f28-ab3f-822707a926e9",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97bcd92d-1816-4ebe-a383-79d828b70e93", "admin@gmail.com", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEPkNFoLGULH3dpBdC28Bv1YIne8SAOb2CZKBFvHQUTzUETdeOZXlxdM318PmF5UJ0A==", "d30574ac-2a0a-4f28-ab3f-822707a926e9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d30574ac-2a0a-4f28-ab3f-822707a926e9",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8043ea5a-ad54-4d81-9246-be2c74eb3d01", "ADMIN@GMAIL.COM", null, "AQAAAAIAAYagAAAAEFN7jbE8w9DKBv+rJvkwXVl52IBQCrZ51jn818RAuux6Y5CdpmjANg89kkw3f0bbbA==", "2d40f95f-a524-4718-ae53-80163382b6e8" });
        }
    }
}
