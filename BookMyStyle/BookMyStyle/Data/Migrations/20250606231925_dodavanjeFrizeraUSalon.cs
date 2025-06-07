using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyStyle.Migrations
{
    /// <inheritdoc />
    public partial class dodavanjeFrizeraUSalon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "SalonID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SalonID",
                table: "AspNetUsers",
                column: "SalonID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Salon_SalonID",
                table: "AspNetUsers",
                column: "SalonID",
                principalTable: "Salon",
                principalColumn: "salonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Salon_SalonID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SalonID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SalonID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "FrizerID",
                table: "Salon",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salon_FrizerID",
                table: "Salon",
                column: "FrizerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Salon_AspNetUsers_FrizerID",
                table: "Salon",
                column: "FrizerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
