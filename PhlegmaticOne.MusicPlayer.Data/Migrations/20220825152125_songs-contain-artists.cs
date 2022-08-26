using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhlegmaticOne.MusicPlayer.Data.Migrations
{
    public partial class songscontainartists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnlineUrl",
                table: "Albums");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimePlayed",
                table: "Collections",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Artists",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Songs_SongId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_SongId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "TimePlayed",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "SongId",
                table: "Artists");

            migrationBuilder.AddColumn<string>(
                name: "OnlineUrl",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
