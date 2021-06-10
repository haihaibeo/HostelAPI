using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class add_prop_service : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ServiceId = table.Column<string>(maxLength: 50, nullable: false),
                    ServiceName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "PropertyWithServices",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    PropertyId = table.Column<string>(maxLength: 50, nullable: false),
                    ServiceId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyWithServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyWithServices_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyWithServices_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyWithServices_PropertyId",
                table: "PropertyWithServices",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyWithServices_ServiceId",
                table: "PropertyWithServices",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyWithServices");

            migrationBuilder.DropTable(
                name: "Service");
        }
    }
}
