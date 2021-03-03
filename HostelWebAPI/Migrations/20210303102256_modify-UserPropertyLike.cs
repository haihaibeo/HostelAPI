using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class modifyUserPropertyLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserPropertyLikes_UserId",
                table: "UserPropertyLikes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPropertyLikes_AspNetUsers_UserId",
                table: "UserPropertyLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPropertyLikes_AspNetUsers_UserId",
                table: "UserPropertyLikes");

            migrationBuilder.DropIndex(
                name: "IX_UserPropertyLikes_UserId",
                table: "UserPropertyLikes");
        }
    }
}
