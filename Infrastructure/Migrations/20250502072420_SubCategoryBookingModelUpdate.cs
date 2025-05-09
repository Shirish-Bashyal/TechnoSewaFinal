using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SubCategoryBookingModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoryBookings_Bookings_BookingId",
                table: "SubCategoryBookings");

            migrationBuilder.DropIndex(
                name: "IX_SubCategoryBookings_BookingId",
                table: "SubCategoryBookings");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "SubCategoryBookings");

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryBookingId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SubCategoryBookingId",
                table: "Bookings",
                column: "SubCategoryBookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_SubCategoryBookings_SubCategoryBookingId",
                table: "Bookings",
                column: "SubCategoryBookingId",
                principalTable: "SubCategoryBookings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_SubCategoryBookings_SubCategoryBookingId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_SubCategoryBookingId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "SubCategoryBookingId",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "SubCategoryBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryBookings_BookingId",
                table: "SubCategoryBookings",
                column: "BookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoryBookings_Bookings_BookingId",
                table: "SubCategoryBookings",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
