using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyStyle.Data.Migrations
{
    /// <inheritdoc />
    public partial class termin2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usluga",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usluga", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Termin",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivSalona = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdresaSalona = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NazivFrizera = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UslugaID = table.Column<int>(type: "int", nullable: false),
                    DatumIVrijeme = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termin", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Termin_Usluga_UslugaID",
                        column: x => x.UslugaID,
                        principalTable: "Usluga",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Termin_UslugaID",
                table: "Termin",
                column: "UslugaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Termin");

            migrationBuilder.DropTable(
                name: "Usluga");
        }
    }
}
