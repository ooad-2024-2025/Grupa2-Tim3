using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyStyle.Data.Migrations
{
    /// <inheritdoc />
    public partial class qr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QRCodeModel",
                columns: table => new
                {
                    QRCodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QRImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverEmailAddress = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    EmailSubject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailMessage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRCodeModel", x => x.QRCodeID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QRCodeModel");
        }
    }
}
