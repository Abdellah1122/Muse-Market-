using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FananeenAPI.Migrations
{
    /// <inheritdoc />
    public partial class b3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NbrCommisionListed",
                table: "Commisions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NbrCommisionListed",
                table: "Commisions");
        }
    }
}
