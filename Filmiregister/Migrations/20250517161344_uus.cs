using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filmiregister.Migrations
{
    /// <inheritdoc />
    public partial class uus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileToDatabase",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FilmID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileToDatabase", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    FilmID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilmTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilmCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilmDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilmRating = table.Column<double>(type: "float", nullable: false),
                    PublicationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FilmDuration = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.FilmID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileToDatabase");

            migrationBuilder.DropTable(
                name: "Films");
        }
    }
}
