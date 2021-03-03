using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelWebAPI.Migrations
{
    public partial class modify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPropertyLike");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "ReservationHistory",
                type: "nvarchar(450)",
                unicode: false,
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyID",
                table: "ReservationHistory",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHistory_UserID",
                table: "ReservationHistory",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHistory_AspNetUsers_UserID",
                table: "ReservationHistory",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHistory_AspNetUsers_UserID",
                table: "ReservationHistory");

            migrationBuilder.DropIndex(
                name: "IX_ReservationHistory_UserID",
                table: "ReservationHistory");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "ReservationHistory",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldUnicode: false,
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyID",
                table: "ReservationHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserPropertyLike",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PropertyID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPropertyLikes", x => new { x.UserID, x.PropertyID });
                    table.ForeignKey(
                        name: "FK_UserPropertyLikes_Properties",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPropertyLike_PropertyID",
                table: "UserPropertyLike",
                column: "PropertyID");
        }
    }
}
