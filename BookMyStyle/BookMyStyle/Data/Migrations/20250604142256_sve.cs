using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyStyle.Data.Migrations
{
    /// <inheritdoc />
    public partial class sve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QRCodeModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QRCodeText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRCodeModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmtpSettings",
                columns: table => new
                {
                    SmtpSettingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    EnableSsl = table.Column<bool>(type: "bit", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmtpSettings", x => x.SmtpSettingsId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usluga_salonID",
                table: "Usluga",
                column: "salonID");

            migrationBuilder.CreateIndex(
                name: "IX_Termin_salonID",
                table: "Termin",
                column: "salonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Termin_Salon_salonID",
                table: "Termin",
                column: "salonID",
                principalTable: "Salon",
                principalColumn: "salonID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usluga_Salon_salonID",
                table: "Usluga",
                column: "salonID",
                principalTable: "Salon",
                principalColumn: "salonID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Termin_Salon_salonID",
                table: "Termin");

            migrationBuilder.DropForeignKey(
                name: "FK_Usluga_Salon_salonID",
                table: "Usluga");

            migrationBuilder.DropTable(
                name: "QRCodeModels");

            migrationBuilder.DropTable(
                name: "SmtpSettings");

            migrationBuilder.DropIndex(
                name: "IX_Usluga_salonID",
                table: "Usluga");

            migrationBuilder.DropIndex(
                name: "IX_Termin_salonID",
                table: "Termin");
        }
    }
}
