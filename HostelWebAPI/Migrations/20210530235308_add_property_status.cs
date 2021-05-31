using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class add_property_status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PropertyStatusId",
                table: "Property",
                maxLength: 50,
                nullable: true,
                defaultValue: "1");

            migrationBuilder.CreateTable(
                name: "PropertyStatus",
                columns: table => new
                {
                    PropertyStatusId = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyStatus", x => x.PropertyStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Property_PropertyStatusId",
                table: "Property",
                column: "PropertyStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_PropertyStatus_PropertyStatusId",
                table: "Property",
                column: "PropertyStatusId",
                principalTable: "PropertyStatus",
                principalColumn: "PropertyStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_PropertyStatus_PropertyStatusId",
                table: "Property");

            migrationBuilder.DropTable(
                name: "PropertyStatus");

            migrationBuilder.DropIndex(
                name: "IX_Property_PropertyStatusId",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "PropertyStatusId",
                table: "Property");
        }
    }
}
