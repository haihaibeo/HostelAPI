using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class add_timestamp_review_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeCreated",
                table: "Reviews",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeUpdated",
                table: "Reviews",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())");

            migrationBuilder.Sql("CREATE OR ALTER TRIGGER dbo.update_timeStamp_review ON dbo.Reviews AFTER INSERT, UPDATE AS BEGIN SET NOCOUNT ON UPDATE dbo.Reviews SET	TimeUpdated = GETDATE() FROM Inserted i WHERE dbo.Reviews.ReviewId = i.ReviewId END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeCreated",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "TimeUpdated",
                table: "Reviews");

            migrationBuilder.Sql("DROP TRIGGER dbo.update_timeStamp_review");
        }
    }
}
