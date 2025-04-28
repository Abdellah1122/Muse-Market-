using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FananeenAPI.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriesOeuvre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesOeuvre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Villes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SousCategoriesOeuvre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategorieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SousCategoriesOeuvre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SousCategoriesOeuvre_CategoriesOeuvre_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "CategoriesOeuvre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatedeNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageArtist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    NbrFoisVisite = table.Column<int>(type: "int", nullable: false),
                    NbrOeuvreVendu = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    artFormId = table.Column<int>(type: "int", nullable: false),
                    villeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artists_ArtForms_artFormId",
                        column: x => x.artFormId,
                        principalTable: "ArtForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Artists_Villes_villeId",
                        column: x => x.villeId,
                        principalTable: "Villes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    villeId = table.Column<int>(type: "int", nullable: false),
                    Addres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postcode = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Villes_villeId",
                        column: x => x.villeId,
                        principalTable: "Villes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Oeuvres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    DateDeCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsLicensed = table.Column<bool>(type: "bit", nullable: false),
                    ImageOeuvre1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageOeuvre2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageOeuvre3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    ScategorieId = table.Column<int>(type: "int", nullable: false),
                    Statut = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oeuvres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oeuvres_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Oeuvres_SousCategoriesOeuvre_ScategorieId",
                        column: x => x.ScategorieId,
                        principalTable: "SousCategoriesOeuvre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commentaires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clientId = table.Column<int>(type: "int", nullable: false),
                    DateCommentaire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdArtist = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commentaires_Clients_clientId",
                        column: x => x.clientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clientId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEstimated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImgPotentiel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commisions_Clients_clientId",
                        column: x => x.clientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    OeuvreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vente_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vente_Oeuvres_OeuvreId",
                        column: x => x.OeuvreId,
                        principalTable: "Oeuvres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artists_artFormId",
                table: "Artists",
                column: "artFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_villeId",
                table: "Artists",
                column: "villeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_villeId",
                table: "Clients",
                column: "villeId");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaires_clientId",
                table: "Commentaires",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "IX_Commisions_clientId",
                table: "Commisions",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "IX_Oeuvres_ArtistId",
                table: "Oeuvres",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Oeuvres_ScategorieId",
                table: "Oeuvres",
                column: "ScategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_SousCategoriesOeuvre_CategorieId",
                table: "SousCategoriesOeuvre",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Vente_ClientId",
                table: "Vente",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Vente_OeuvreId",
                table: "Vente",
                column: "OeuvreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commentaires");

            migrationBuilder.DropTable(
                name: "Commisions");

            migrationBuilder.DropTable(
                name: "Vente");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Oeuvres");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "SousCategoriesOeuvre");

            migrationBuilder.DropTable(
                name: "ArtForms");

            migrationBuilder.DropTable(
                name: "Villes");

            migrationBuilder.DropTable(
                name: "CategoriesOeuvre");
        }
    }
}
