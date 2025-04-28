using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FananeenAPI.Migrations
{
    /// <inheritdoc />
    public partial class b1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WantedReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    SousCategorieOeuvreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commisions_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commisions_SousCategoriesOeuvre_SousCategorieOeuvreId",
                        column: x => x.SousCategorieOeuvreId,
                        principalTable: "SousCategoriesOeuvre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commisions_ClientId",
                table: "Commisions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Commisions_SousCategorieOeuvreId",
                table: "Commisions",
                column: "SousCategorieOeuvreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commisions");
        }
    }
}
