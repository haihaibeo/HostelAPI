using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class add_property_fees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CleaningFee",
                table: "Property",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ServiceFee",
                table: "Property",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CleaningFee",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "ServiceFee",
                table: "Property");
        }
    }
}
