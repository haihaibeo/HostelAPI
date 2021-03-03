using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class modify1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPropertyLikes",
                columns: table => new
                {
                    UserPropertyId = table.Column<string>(maxLength: 50, nullable: false),
                    PropertyId = table.Column<string>(maxLength: 50, nullable: true),
                    UserId = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPropertyLikes", x => x.UserPropertyId);
                    table.ForeignKey(
                        name: "FK_UserPropertyLikes_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPropertyLikes_PropertyId",
                table: "UserPropertyLikes",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPropertyLikes");
        }
    }
}
