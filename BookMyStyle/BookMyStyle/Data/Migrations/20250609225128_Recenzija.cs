using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyStyle.Migrations
{
    /// <inheritdoc />
    public partial class Recenzija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "salonID",
                table: "Recenzija",
                newName: "SalonID");

            migrationBuilder.RenameColumn(
                name: "korisnikID",
                table: "Recenzija",
                newName: "KorisnikID");

            migrationBuilder.AlterColumn<string>(
                name: "NazivSalona",
                table: "Termin",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NazivFrizera",
                table: "Termin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "AdresaSalona",
                table: "Termin",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "SalonID",
                table: "Recenzija",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "KorisnikID",
                table: "Recenzija",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_KorisnikID",
                table: "Recenzija",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_SalonID",
                table: "Recenzija",
                column: "SalonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzija_AspNetUsers_KorisnikID",
                table: "Recenzija",
                column: "KorisnikID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzija_Salon_SalonID",
                table: "Recenzija",
                column: "SalonID",
                principalTable: "Salon",
                principalColumn: "salonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recenzija_AspNetUsers_KorisnikID",
                table: "Recenzija");

            migrationBuilder.DropForeignKey(
                name: "FK_Recenzija_Salon_SalonID",
                table: "Recenzija");

            migrationBuilder.DropIndex(
                name: "IX_Recenzija_KorisnikID",
                table: "Recenzija");

            migrationBuilder.DropIndex(
                name: "IX_Recenzija_SalonID",
                table: "Recenzija");

            migrationBuilder.RenameColumn(
                name: "SalonID",
                table: "Recenzija",
                newName: "salonID");

            migrationBuilder.RenameColumn(
                name: "KorisnikID",
                table: "Recenzija",
                newName: "korisnikID");

            migrationBuilder.AlterColumn<string>(
                name: "NazivSalona",
                table: "Termin",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NazivFrizera",
                table: "Termin",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdresaSalona",
                table: "Termin",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "salonID",
                table: "Recenzija",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "korisnikID",
                table: "Recenzija",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
