using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_api.Migrations
{
    /// <inheritdoc />
    public partial class BlogContentUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlHandel",
                table: "BlogPosts",
                newName: "UrlHandle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlHandle",
                table: "BlogPosts",
                newName: "UrlHandel");
        }
    }
}
