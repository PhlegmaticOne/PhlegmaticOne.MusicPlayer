using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhlegmaticOne.MusicPlayer.Data.Migrations
{
    public partial class genresdescriptiondeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Genres");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Genres",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");
        }
    }
}
