using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class Add_people_count_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdultNum",
                table: "ReservationHistory",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "ChildrenNum",
                table: "ReservationHistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InfantNum",
                table: "ReservationHistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxPeople",
                table: "Property",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultNum",
                table: "ReservationHistory");

            migrationBuilder.DropColumn(
                name: "ChildrenNum",
                table: "ReservationHistory");

            migrationBuilder.DropColumn(
                name: "InfantNum",
                table: "ReservationHistory");

            migrationBuilder.DropColumn(
                name: "MaxPeople",
                table: "Property");
        }
    }
}
