using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bookingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Photos_Posts_PostId", table: "Photos");

            migrationBuilder.DropPrimaryKey(name: "PK_Photos", table: "Photos");

            migrationBuilder.DropColumn(name: "Status", table: "Posts");

            migrationBuilder.RenameTable(name: "Photos", newName: "PhotoPath");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_PostId",
                table: "PhotoPath",
                newName: "IX_PhotoPath_PostId"
            );

            migrationBuilder.AddPrimaryKey(name: "PK_PhotoPath", table: "PhotoPath", column: "Id");

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Technicians",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SecondPhoneNumber = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false
                    ),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technicians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technicians_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "TimeFrames",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeFrames", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "PostBids",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolutionDescription = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false
                    ),
                    EstimationPrice = table.Column<double>(type: "float", nullable: false),
                    ServiceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    TechnicianId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostBids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostBids_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_PostBids_Technicians_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PostBidId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_PostBids_PostBidId",
                        column: x => x.PostBidId,
                        principalTable: "PostBids",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ByConsumer = table.Column<bool>(type: "bit", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "SubCategoryBookings",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    Lattitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    ServiceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeFrameId = table.Column<int>(type: "int", nullable: false),
                    ConsumerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TechnicianId = table.Column<int>(type: "int", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoryBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategoryBookings_AspNetUsers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_SubCategoryBookings_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_SubCategoryBookings_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction
                    );
                    table.ForeignKey(
                        name: "FK_SubCategoryBookings_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction
                    );
                    table.ForeignKey(
                        name: "FK_SubCategoryBookings_Technicians_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Technicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_SubCategoryBookings_TimeFrames_TimeFrameId",
                        column: x => x.TimeFrameId,
                        principalTable: "TimeFrames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PostBidId",
                table: "Bookings",
                column: "PostBidId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_PostBids_PostId",
                table: "PostBids",
                column: "PostId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_PostBids_TechnicianId",
                table: "PostBids",
                column: "TechnicianId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookingId",
                table: "Reviews",
                column: "BookingId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryBookings_BookingId",
                table: "SubCategoryBookings",
                column: "BookingId",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryBookings_CategoryId",
                table: "SubCategoryBookings",
                column: "CategoryId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryBookings_ConsumerId",
                table: "SubCategoryBookings",
                column: "ConsumerId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryBookings_SubCategoryId",
                table: "SubCategoryBookings",
                column: "SubCategoryId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryBookings_TechnicianId",
                table: "SubCategoryBookings",
                column: "TechnicianId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryBookings_TimeFrameId",
                table: "SubCategoryBookings",
                column: "TimeFrameId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Technicians_UserId",
                table: "Technicians",
                column: "UserId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoPath_Posts_PostId",
                table: "PhotoPath",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_PhotoPath_Posts_PostId", table: "PhotoPath");

            migrationBuilder.DropTable(name: "Reviews");

            migrationBuilder.DropTable(name: "SubCategoryBookings");

            migrationBuilder.DropTable(name: "Bookings");

            migrationBuilder.DropTable(name: "SubCategories");

            migrationBuilder.DropTable(name: "TimeFrames");

            migrationBuilder.DropTable(name: "PostBids");

            migrationBuilder.DropTable(name: "Technicians");

            migrationBuilder.DropPrimaryKey(name: "PK_PhotoPath", table: "PhotoPath");

            migrationBuilder.RenameTable(name: "PhotoPath", newName: "Photos");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoPath_PostId",
                table: "Photos",
                newName: "IX_Photos_PostId"
            );

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddPrimaryKey(name: "PK_Photos", table: "Photos", column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Posts_PostId",
                table: "Photos",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
