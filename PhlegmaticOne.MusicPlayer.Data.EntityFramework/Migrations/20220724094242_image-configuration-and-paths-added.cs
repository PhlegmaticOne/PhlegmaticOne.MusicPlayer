using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhlegmaticOne.MusicPlayer.Data.Migrations
{
    public partial class imageconfigurationandpathsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalUrl",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OnlineUrl",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OnlineUrl",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AlbumCovers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cover = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumCovers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlbumCovers_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumCovers_AlbumId",
                table: "AlbumCovers",
                column: "AlbumId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumCovers");

            migrationBuilder.DropColumn(
                name: "LocalUrl",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "OnlineUrl",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "OnlineUrl",
                table: "Albums");
        }
    }
}
