using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class add_reviews_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserPropertyLikes_PropertyId",
                table: "UserPropertyLikes");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<string>(maxLength: 50, nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    PropId = table.Column<string>(maxLength: 50, nullable: false),
                    ReservationId = table.Column<string>(maxLength: 50, nullable: true),
                    Star = table.Column<int>(nullable: false, defaultValue: 0),
                    ReviewComment = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Property_PropId",
                        column: x => x.PropId,
                        principalTable: "Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPropertyLikes_PropertyId_UserId",
                table: "UserPropertyLikes",
                columns: new[] { "PropertyId", "UserId" },
                unique: true,
                filter: "[PropertyId] IS NOT NULL AND [UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PropId_UserId",
                table: "Reviews",
                columns: new[] { "PropId", "UserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_UserPropertyLikes_PropertyId_UserId",
                table: "UserPropertyLikes");

            migrationBuilder.CreateIndex(
                name: "IX_UserPropertyLikes_PropertyId",
                table: "UserPropertyLikes",
                column: "PropertyId");
        }
    }
}
