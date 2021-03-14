using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class add_O2O_PropertyService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyServices",
                columns: table => new
                {
                    PropertyId = table.Column<string>(maxLength: 50, nullable: false),
                    Wifi = table.Column<bool>(nullable: false, defaultValue: false),
                    Kitchen = table.Column<bool>(nullable: false, defaultValue: false),
                    Breakfast = table.Column<bool>(nullable: false, defaultValue: false),
                    PetAllowed = table.Column<bool>(nullable: false, defaultValue: false),
                    FreeParking = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyServices", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_PropertyServices_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyServices");
        }
    }
}
