using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhlegmaticOne.MusicPlayer.Data.Migrations
{
    public partial class propertiesrenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Genres",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Artists",
                newName: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Genres",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Artists",
                newName: "Name");
        }
    }
}
