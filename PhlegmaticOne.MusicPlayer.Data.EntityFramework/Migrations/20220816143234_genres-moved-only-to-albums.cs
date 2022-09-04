using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhlegmaticOne.MusicPlayer.Data.Migrations
{
    public partial class genresmovedonlytoalbums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionBaseGenre");

            migrationBuilder.CreateTable(
                name: "AlbumGenre",
                columns: table => new
                {
                    AlbumsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumGenre", x => new { x.AlbumsId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_AlbumGenre_Albums_AlbumsId",
                        column: x => x.AlbumsId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumGenre_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumGenre_GenresId",
                table: "AlbumGenre",
                column: "GenresId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumGenre");

            migrationBuilder.CreateTable(
                name: "CollectionBaseGenre",
                columns: table => new
                {
                    AlbumsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionBaseGenre", x => new { x.AlbumsId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_CollectionBaseGenre_Collections_AlbumsId",
                        column: x => x.AlbumsId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionBaseGenre_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionBaseGenre_GenresId",
                table: "CollectionBaseGenre",
                column: "GenresId");
        }
    }
}
