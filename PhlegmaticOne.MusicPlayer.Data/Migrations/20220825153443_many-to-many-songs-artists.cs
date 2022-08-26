using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhlegmaticOne.MusicPlayer.Data.Migrations
{
    public partial class manytomanysongsartists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Songs_SongId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_SongId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "SongId",
                table: "Artists");

            migrationBuilder.CreateTable(
                name: "ArtistSong",
                columns: table => new
                {
                    ArtistsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SongsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistSong", x => new { x.ArtistsId, x.SongsId });
                    table.ForeignKey(
                        name: "FK_ArtistSong_Artists_ArtistsId",
                        column: x => x.ArtistsId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistSong_Songs_SongsId",
                        column: x => x.SongsId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistSong_SongsId",
                table: "ArtistSong",
                column: "SongsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistSong");

            migrationBuilder.AddColumn<Guid>(
                name: "SongId",
                table: "Artists",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artists_SongId",
                table: "Artists",
                column: "SongId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Songs_SongId",
                table: "Artists",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id");
        }
    }
}
