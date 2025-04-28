using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FananeenAPI.Migrations
{
    /// <inheritdoc />
    public partial class b6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageCommision",
                table: "Commisions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageCommision",
                table: "Commisions");
        }
    }
}
