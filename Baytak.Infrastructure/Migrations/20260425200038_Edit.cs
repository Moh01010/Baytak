using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Baytak.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Bookings",
                newName: "BookingDate");

            migrationBuilder.AddColumn<string>(
                name: "AgentId",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AgentId",
                table: "Bookings",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_AgentId",
                table: "Bookings",
                column: "AgentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_AgentId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_AgentId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookingDate",
                table: "Bookings",
                newName: "Date");
        }
    }
}
