using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhlegmaticOne.MusicPlayer.Data.Migrations
{
    public partial class coverstablerenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumCovers_Collections_AlbumId",
                table: "AlbumCovers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlbumCovers",
                table: "AlbumCovers");

            migrationBuilder.RenameTable(
                name: "AlbumCovers",
                newName: "CollectionCovers");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumCovers_AlbumId",
                table: "CollectionCovers",
                newName: "IX_CollectionCovers_AlbumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollectionCovers",
                table: "CollectionCovers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionCovers_Collections_AlbumId",
                table: "CollectionCovers",
                column: "AlbumId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionCovers_Collections_AlbumId",
                table: "CollectionCovers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollectionCovers",
                table: "CollectionCovers");

            migrationBuilder.RenameTable(
                name: "CollectionCovers",
                newName: "AlbumCovers");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionCovers_AlbumId",
                table: "AlbumCovers",
                newName: "IX_AlbumCovers_AlbumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlbumCovers",
                table: "AlbumCovers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumCovers_Collections_AlbumId",
                table: "AlbumCovers",
                column: "AlbumId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
