using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class modifyuserrelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnerID",
                table: "Property",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_Property_OwnerID",
                table: "Property",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserID",
                table: "Comment",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_UserID",
                table: "Comment",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_AspNetUsers_OwnerID",
                table: "Property",
                column: "OwnerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_UserID",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_AspNetUsers_OwnerID",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_OwnerID",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Comment_UserID",
                table: "Comment");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerID",
                table: "Property",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
