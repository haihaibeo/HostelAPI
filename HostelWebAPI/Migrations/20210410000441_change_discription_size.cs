using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class change_discription_size : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Introduction",
                table: "Property",
                maxLength: 2000,
                nullable: true,
                defaultValueSql: "(N'Property Introduction')",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldDefaultValueSql: "(N'Property Introduction')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Introduction",
                table: "Property",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValueSql: "(N'Property Introduction')",
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true,
                oldDefaultValueSql: "(N'Property Introduction')");
        }
    }
}
