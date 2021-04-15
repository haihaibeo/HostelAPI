using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class cr_trigger_update_timestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE OR ALTER TRIGGER dbo.update_timeStamp ON dbo.Property AFTER INSERT, UPDATE AS BEGIN SET NOCOUNT ON UPDATE dbo.Property SET	TimeUpdated = GETDATE() FROM Inserted i WHERE dbo.Property.PropertyID = i.PropertyID END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER dbo.update_timeStamp");
        }
    }
}
