using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_api.Migrations.AuthDB
{
    /// <inheritdoc />
    public partial class FixAdminConcurrencyStamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d30574ac-2a0a-4f28-ab3f-822707a926e9",
                column: "ConcurrencyStamp",
                value: "d30574ac-2a0a-4f28-ab3f-822707a926e9");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d30574ac-2a0a-4f28-ab3f-822707a926e9",
                column: "ConcurrencyStamp",
                value: "90244ef5-d15a-4b14-8588-cb81a5afadfd");
        }
    }
}
