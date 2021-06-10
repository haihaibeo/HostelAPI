using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class del_old_propServ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyServices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyServices",
                columns: table => new
                {
                    PropertyId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Breakfast = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FreeParking = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Kitchen = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PetAllowed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Wifi = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
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
    }
}
