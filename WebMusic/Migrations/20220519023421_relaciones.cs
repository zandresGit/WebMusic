using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMusic.Migrations
{
    public partial class relaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "Cancions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GeneroId",
                table: "Bandas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BandaId",
                table: "Albums",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cancions_AlbumId",
                table: "Cancions",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Bandas_GeneroId",
                table: "Bandas",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_BandaId",
                table: "Albums",
                column: "BandaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Bandas_BandaId",
                table: "Albums",
                column: "BandaId",
                principalTable: "Bandas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bandas_Generos_GeneroId",
                table: "Bandas",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cancions_Albums_AlbumId",
                table: "Cancions",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Bandas_BandaId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Bandas_Generos_GeneroId",
                table: "Bandas");

            migrationBuilder.DropForeignKey(
                name: "FK_Cancions_Albums_AlbumId",
                table: "Cancions");

            migrationBuilder.DropIndex(
                name: "IX_Cancions_AlbumId",
                table: "Cancions");

            migrationBuilder.DropIndex(
                name: "IX_Bandas_GeneroId",
                table: "Bandas");

            migrationBuilder.DropIndex(
                name: "IX_Albums_BandaId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Cancions");

            migrationBuilder.DropColumn(
                name: "GeneroId",
                table: "Bandas");

            migrationBuilder.DropColumn(
                name: "BandaId",
                table: "Albums");
        }
    }
}
