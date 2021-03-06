using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class add_service_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ServiceId = table.Column<string>(maxLength: 50, nullable: false),
                    ServiceName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "PropertyService",
                columns: table => new
                {
                    PropertyServiceId = table.Column<string>(maxLength: 50, nullable: false),
                    PropertyId = table.Column<string>(maxLength: 50, nullable: false),
                    ServiceId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyService", x => x.PropertyServiceId);
                    table.ForeignKey(
                        name: "FK_PropertyService_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyService_PropertyId",
                table: "PropertyService",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyService_ServiceId",
                table: "PropertyService",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyService");

            migrationBuilder.DropTable(
                name: "Service");
        }
    }
}
