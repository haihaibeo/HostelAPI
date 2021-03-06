using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class property__change_review_count : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Property");

            migrationBuilder.AddColumn<int>(
                name: "TotalReview",
                table: "Property",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalStar",
                table: "Property",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalReview",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "TotalStar",
                table: "Property");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Property",
                type: "float",
                nullable: false,
                defaultValueSql: "((5.0))");
        }
    }
}
