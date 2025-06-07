using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyStyle.Migrations
{
    /// <inheritdoc />
    public partial class dodavanjeFKova : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Termin: KorisnikID
            migrationBuilder.AddColumn<string>(
                name: "KorisnikID",
                table: "Termin",
                type: "nvarchar(450)",
                nullable: true);

            // Termin: FrizerID
            migrationBuilder.AddColumn<string>(
                name: "FrizerID",
                table: "Termin",
                type: "nvarchar(450)",
                nullable: true);

            // Salon: FrizerID ➤ MORA biti prije FK i Index
            migrationBuilder.AddColumn<string>(
                name: "FrizerID",
                table: "Salon",
                type: "nvarchar(450)",
                nullable: true);

            // Indeksi
            migrationBuilder.CreateIndex(
                name: "IX_Termin_KorisnikID",
                table: "Termin",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Termin_FrizerID",
                table: "Termin",
                column: "FrizerID");

            migrationBuilder.CreateIndex(
                name: "IX_Salon_FrizerID",
                table: "Salon",
                column: "FrizerID");

            // Foreign ključevi
            migrationBuilder.AddForeignKey(
                name: "FK_Termin_AspNetUsers_KorisnikID",
                table: "Termin",
                column: "KorisnikID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Termin_AspNetUsers_FrizerID",
                table: "Termin",
                column: "FrizerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Salon_AspNetUsers_FrizerID",
                table: "Salon",
                column: "FrizerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }



        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
